using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? ImageUrl { get; set; }
        public string? Details { get; set; }
        public string? Address { get; set; }
    }
}
