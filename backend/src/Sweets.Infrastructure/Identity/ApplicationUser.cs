using Microsoft.AspNetCore.Identity;

namespace Sweets.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}