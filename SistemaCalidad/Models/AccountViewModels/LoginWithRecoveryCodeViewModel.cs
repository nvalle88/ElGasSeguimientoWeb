#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace ElGasSeguimientoWeb.Models
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}
