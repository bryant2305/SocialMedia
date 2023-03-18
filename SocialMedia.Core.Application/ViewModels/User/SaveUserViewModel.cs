using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca el usuario por favor")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Introduzca la contraseña por favor")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no son iguales")]
        [Required(ErrorMessage = "Introduzca la contraseña por favor")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Introduzca un nombre por favor")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Introduzca un apellido por favor")]
        [DataType(DataType.Text)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Introduzca un correo por favor")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduzca el telefono por favor")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        public string? Photo { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        public bool Activo { get; set; }

    }
}
