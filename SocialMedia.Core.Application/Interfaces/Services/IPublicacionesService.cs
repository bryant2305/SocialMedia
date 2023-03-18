using SocialMedia.Core.Application.ViewModels.Publicaciones;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Interfaces.Services
{
    public interface IPublicacionesService : IGenericService<SavePublicacionesViewModel, PublicacionesViewModel,Publicaciones>
    {
        Task<List<PublicacionesViewModel>> GetAllMyPublications();
        Task<PublicacionesViewModel> GetPostsandDetails(int id);

        Task<List<PublicacionesViewModel>> GetAllMyAmigosPost(int friendid);

        
    }
}
