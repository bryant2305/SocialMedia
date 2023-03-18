using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Introduzca el usuario por favor")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Introduzca contraseña por favor")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
