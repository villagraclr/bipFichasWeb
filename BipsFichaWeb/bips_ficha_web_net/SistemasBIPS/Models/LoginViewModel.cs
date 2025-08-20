using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SistemasBIPS.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class OlvidoContraseñaViewModel
    {
        [Required]
        public string Email { get; set; }
    }

    public class CambioPassViewModel
    {
        [Required]
        [HiddenInput]
        public string idUsuario { get; set; }

        [Required(ErrorMessage = "Ingrese su actual contraseña")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña tiene que tener como mínimo 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ingrese nueva contraseña")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña tiene que tener como mínimo 6 caracteres")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Reingrese nueva contraseña")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña tiene que tener como mínimo 6 caracteres")]
        public string ConfirmNewPassword { get; set; }
    }

    public class ResetPassViewModel
    {
        [Required]
        [HiddenInput]
        public string idUsuario { get; set; }

        [Required]
        [HiddenInput]
        public string code { get; set; }

        [Required(ErrorMessage = "Ingrese nueva contraseña")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña tiene que tener como mínimo 6 caracteres")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Reingrese nueva contraseña")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña tiene que tener como mínimo 6 caracteres")]
        public string ConfirmNewPassword { get; set; }
    }
}