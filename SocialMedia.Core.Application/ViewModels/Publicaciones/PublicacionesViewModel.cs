using SocialMedia.Core.Application.ViewModels.Comentarios;
using SocialMedia.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.Publicaciones
{
    public class PublicacionesViewModel
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime? Created { get; set; }
        public UserViewModel? User { get; set; }
        public ICollection<ComentariosViewModel>? Comentarios { get; set; }

    }
}
