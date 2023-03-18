using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Core.Application.ViewModels.User
{
    public class OlvideContraViewModel
    {
        [Required(ErrorMessage = "Introduzca el usuario por favor")]
        [DataType(DataType.Text)]
        public string? Username { get; set; }
    }
}
