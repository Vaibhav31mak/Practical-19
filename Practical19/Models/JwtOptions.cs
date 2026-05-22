namespace Practical19.Models;

public class JwtOptions
{
    [Required]
    public string Issuer { get; set; } = string.Empty;
    [Required]
    public string Audience { get; set; } = string.Empty;
    [Required]
    public string Key { get; set; } = string.Empty;
    public int ExpiresMinutes { get; set; } = 60;
}
