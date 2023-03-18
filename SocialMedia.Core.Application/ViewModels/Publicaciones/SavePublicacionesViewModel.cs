using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SocialMedia.Core.Application.ViewModels.Publicaciones
{
    public class SavePublicacionesViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "La publicacion no puede estar vacia.")]
        [DataType(DataType.Text)]
        public string? Descripcion { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile? File { get; set; }

        public string? NuevoComentario { get; set; }
        public int Idpost { get; set; }

    }
}
