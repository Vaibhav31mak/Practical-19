namespace Practical19.Data;

/// <summary>
/// Entity Framework database context for identity data.
/// </summary>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
{
}
