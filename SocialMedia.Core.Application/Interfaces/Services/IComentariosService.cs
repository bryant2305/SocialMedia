using SocialMedia.Core.Application.ViewModels.Comentarios;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Interfaces.Services
{
    public interface IComentariosService : IGenericService<SaveComentariosViewModel, ComentariosViewModel,Comentarios>
    {
      
    }
}
