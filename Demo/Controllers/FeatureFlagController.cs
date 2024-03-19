using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    /// <summary>
    /// Represents a controller for feature flags.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureFlagController : ControllerBase
    {
        private readonly IFeatureFlagService _featureFlagService;

        public FeatureFlagController(IFeatureFlagService featureFlagService)
        {
            _featureFlagService = featureFlagService;
        }

        /// <summary>
        /// Retrieves a feature flag by its name.
        /// </summary>
        /// <param name="name">The name of the feature flag to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the asynchronous operation.</returns>
        /// <response code="200">The feature flag with the specified name.</response>
        /// <response code="404">The feature flag with the specified name was not found.</response>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetFeatureFlagByNameAsync(string name)
        {
            var featureFlag = await _featureFlagService.GetFeatureFlagByNameAsync(name);
            if (featureFlag == null)
            {
                return NotFound();
            }

            return Ok(featureFlag);
        }

        /// <summary>
        /// Retrieves all feature flags asynchronously.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the asynchronous operation.</returns>
        /// <response code="200">The collection of feature flags.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllFeatureFlagsAsync()
        {
            // Retrieves all feature flags asynchronously
            var featureFlags = await _featureFlagService.GetAllFeatureFlagsAsync();
            return Ok(featureFlags);
        }

        /// <summary>
        /// Adds multiple feature flags.
        /// </summary>
        /// <param name="featureFlag">The collection of feature flags to add.</param>
        /// <returns>An <see cref="IActionResult"/> representing the response of the operation.</returns>
        /// <response code="200">The newly added feature flags.</response>
        [HttpPost]
        public async Task<IActionResult> AddFeatureFlagsAsync([FromBody] IEnumerable<FeatureFlag> featureFlag)
        {
            var addedFeatureFlags = await _featureFlagService.AddFeatureFlagsAsync(featureFlag);
            return Ok(addedFeatureFlags);
        }

        /// <summary>
        /// Updates a feature flag.
        /// </summary>
        /// <param name="featureFlag">The feature flag to update.</param>
        /// <returns>An <see cref="IActionResult"/> representing the response of the operation.</returns>
        /// <response code="200">The updated feature flag.</response>
        /// <response code="400">The feature flag could not be updated.</response>
        [HttpPut]
        public async Task<IActionResult> UpdateFeatureFlagAsync([FromBody] FeatureFlag featureFlag)
        {
            try
            {
                var updatedFeatureFlag = await _featureFlagService.UpdateFeatureFlagAsync(featureFlag);
                return Ok(updatedFeatureFlag);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}