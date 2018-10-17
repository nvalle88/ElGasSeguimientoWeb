using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SistemaCalidad.Utils;

namespace SistemaCalidad.Models
{
    public class ConfirmAccount
    {
        [Required(ErrorMessage = Validaciones.Requerido)]
        [EmailAddress(ErrorMessage = Validaciones.FormatoCorreo)]
        [Display(Name = "Correo electrónico")]
        [StringLength(maximumLength: 256, ErrorMessage = Validaciones.LongitudString)]
        public string Email { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("Password", ErrorMessage = "La Nueva contraseña y Confirmar nueva contraseña no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
