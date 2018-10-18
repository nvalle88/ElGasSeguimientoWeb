#region Using

using System.ComponentModel.DataAnnotations;
using ElGasSeguimientoWeb.Utils;

#endregion

namespace ElGasSeguimientoWeb.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage =Validaciones.Requerido)]
        [EmailAddress(ErrorMessage =Validaciones.FormatoCorreo)]
        [Display(Name ="Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
