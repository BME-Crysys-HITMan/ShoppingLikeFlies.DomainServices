namespace ShoppingLikeFiles.DomainServices.Options;

public class CaffValidatorOptions
{
    /// <summary>
    /// Specifies the validator plugin installation location.
    /// </summary>
    public string Validator { get; set; } = string.Empty;

    /// <summary>
    /// Secifies a folder that the CAFF_Processor should put its outputs at.
    /// </summary>
    public string GeneratorDir { get; set; } = string.Empty;
}

