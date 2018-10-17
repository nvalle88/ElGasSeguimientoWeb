#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace SistemaCalidad.Models
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
