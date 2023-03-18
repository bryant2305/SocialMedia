using SocialMedia.Core.Application.ViewModels.Publicaciones;
using SocialMedia.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.Comentarios
{
    public class ComentariosViewModel
    {
        public string? Descripcion { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public UserViewModel? User { get; set; }
        public PublicacionesViewModel? Post { get; set; }

    }
}
