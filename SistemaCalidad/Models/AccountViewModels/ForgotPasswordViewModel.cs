#region Using

using System.ComponentModel.DataAnnotations;
using ElGasSeguimientoWeb.Utils;

#endregion

namespace ElGasSeguimientoWeb.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage =Validaciones.Requerido)]
        [EmailAddress(ErrorMessage =Validaciones.FormatoCorreo)]
        [Display(Name ="Correo elctr�nico")]
        public string Email { get; set; }
    }
}
