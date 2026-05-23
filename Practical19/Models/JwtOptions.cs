namespace Practical19.Models;

/// <summary>
/// Represents JWT configuration settings.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Gets or sets the token issuer.
    /// </summary>
    [Required]
    public string Issuer { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the token audience.
    /// </summary>
    [Required]
    public string Audience { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the signing key.
    /// </summary>
    [Required]
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the token expiration in minutes.
    /// </summary>
    public int ExpiresMinutes { get; set; } = 60;
}
