using System.ComponentModel.DataAnnotations;

public class FeatureFlag
{
    [Required, MinLength(5)]
    public string Name { get; set; }
    public bool IsEnabled { get; set; }

}