using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.Comentarios
{
    public class SaveComentariosViewModel
    {
        [Required(ErrorMessage = "el comentario debe contener algo")]
        [DataType(DataType.Text)]
        public string? Descripcion { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
