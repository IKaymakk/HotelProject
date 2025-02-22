using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer
{
    public class ApplicationUser : IdentityUser<Guid>  
    {
        public string? FullName { get; set; }
    }
}
