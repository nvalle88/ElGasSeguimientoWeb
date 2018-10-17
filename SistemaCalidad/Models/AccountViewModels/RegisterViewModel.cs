#region Using

using System.ComponentModel.DataAnnotations;
using SistemaCalidad.Utils;

#endregion

namespace SistemaCalidad.Models
{
    public class RegisterViewModel
    {

        public string Id { get; set; }

        [Required(ErrorMessage =Validaciones.Requerido)]
        [EmailAddress(ErrorMessage =Validaciones.FormatoCorreo)]
        [Display(Name = "Correo electrónico")]
        [StringLength(maximumLength:256,ErrorMessage =Validaciones.LongitudString)]
        public string Email { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [Display(Name = "Nombres")]
        [StringLength(maximumLength: 256, ErrorMessage = Validaciones.LongitudString)]
        public string Name { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [Display(Name = "Apellidos")]
        [StringLength(maximumLength: 256, ErrorMessage = Validaciones.LongitudString)]
        public string LastName { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(maximumLength: 256, ErrorMessage = Validaciones.LongitudString)]
        public string Address { get; set; }

        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = "Rol")]
        public string IdRol { get; set; }

        public bool Status { get; set; }
    }
}
