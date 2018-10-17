#region Using

using Microsoft.AspNetCore.Identity;

#endregion

namespace SistemaCalidad.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public bool Status { get; set; }
    }
}
