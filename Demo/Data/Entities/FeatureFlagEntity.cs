using System.ComponentModel.DataAnnotations;

namespace Demo.Data.Entities
{
    public class FeatureFlagEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public FeatureFlag ToModel()
        {
            return new FeatureFlag
            {
                Name = Name,
                IsEnabled = IsEnabled
            };
        }
    }
}
