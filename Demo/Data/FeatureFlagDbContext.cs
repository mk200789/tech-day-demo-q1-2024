using Demo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    /// <summary>
    /// Represents the database context for feature flags.
    /// </summary>
    public class FeatureFlagDbContext : DbContext
    {
        public FeatureFlagDbContext(DbContextOptions<FeatureFlagDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the feature flags.
        /// </summary>
        public virtual DbSet<FeatureFlagEntity> FeatureFlags { get; set; }
    }
}
