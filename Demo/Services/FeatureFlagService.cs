using Demo.Data;
using Demo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
    public interface IFeatureFlagService
    {
        Task<FeatureFlag?> GetFeatureFlagByNameAsync(string name);
        Task<IEnumerable<FeatureFlag>> AddFeatureFlagsAsync(IEnumerable<FeatureFlag> featureFlag);
        Task<FeatureFlag> UpdateFeatureFlagAsync(FeatureFlag featureFlag);
        Task<IEnumerable<FeatureFlag>> GetAllFeatureFlagsAsync();
    }

    /// <summary>
    /// Represents a service for feature flags.
    /// </summary>
    public class FeatureFlagService : IFeatureFlagService
    {
        private readonly FeatureFlagDbContext _dbContext;

        public FeatureFlagService(FeatureFlagDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a feature flag by its name.
        /// </summary>
        /// <param name="name">The name of the feature flag to retrieve.</param>
        /// <returns>The feature flag with the specified name, or null if not found.</returns>
        public async Task<FeatureFlag?> GetFeatureFlagByNameAsync(string name)
        {
            var flags = await _dbContext.FeatureFlags.FirstOrDefaultAsync(f => f.Name == name);
            return flags?.ToModel();
        }

        public async Task<IEnumerable<FeatureFlag>> AddFeatureFlagsAsync(IEnumerable<FeatureFlag> featureFlag)
        {
            var entities = featureFlag.Select(f => new FeatureFlagEntity
            {
                Name = f.Name,
                IsEnabled = f.IsEnabled,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            });

            await _dbContext.FeatureFlags.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

            return entities.Select(f => f.ToModel());
        }


        public async Task<FeatureFlag> UpdateFeatureFlagAsync(FeatureFlag featureFlag)
        {
            var entity = await _dbContext.FeatureFlags.FirstOrDefaultAsync(f => f.Name == featureFlag.Name);
            if (entity == null)
            {
                throw new InvalidOperationException($"Feature flag '{featureFlag.Name}' not found.");
            }

            entity.IsEnabled = featureFlag.IsEnabled;
            entity.UpdatedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return entity.ToModel();
        }


        public async Task<IEnumerable<FeatureFlag>> GetAllFeatureFlagsAsync()
        {
            var flags = await _dbContext.FeatureFlags.ToListAsync();
            return flags.Select(f => f.ToModel());
        }
    }
}