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

        [HttpGet]
        public async Task<IActionResult> GetAllFeatureFlagsAsync()
        {
            // Retrieves all feature flags asynchronously
            var featureFlags = await _featureFlagService.GetAllFeatureFlagsAsync();
            return Ok(featureFlags);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeatureFlagsAsync([FromBody] IEnumerable<FeatureFlag> featureFlag)
        {
            var addedFeatureFlags = await _featureFlagService.AddFeatureFlagsAsync(featureFlag);
            return Ok(addedFeatureFlags);
        }

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