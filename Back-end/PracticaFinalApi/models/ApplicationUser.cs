using Microsoft.AspNetCore.Identity;

namespace PracticaFinalApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
