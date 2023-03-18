using AutoMapper;
using SocialMedia.Core.Application.ViewModels.Comentarios;
using SocialMedia.Core.Application.ViewModels.Publicaciones;
using SocialMedia.Core.Application.ViewModels.User;
using SocialMedia.Core.Application.ViewModels.Friends;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region  users
            CreateMap<User, UserViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<User, SaveUserViewModel>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
               .ForMember(dest => dest.Publicaciones, opt => opt.Ignore())
               .ForMember(dest => dest.Friends, opt => opt.Ignore());

            CreateMap<UserViewModel, SaveUserViewModel>()
               .ReverseMap()
               .ForMember(dest => dest.Post, opt => opt.Ignore())
               .ForMember(dest => dest.Friendship, opt => opt.Ignore());
           


            #endregion

            #region publicaciones
            CreateMap<Publicaciones, PublicacionesViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Publicaciones, SavePublicacionesViewModel>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.NuevoComentario, opt => opt.Ignore())
                .ForMember(dest => dest.Idpost, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region comentarios
            CreateMap<Comentarios, ComentariosViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Comentarios, SaveComentariosViewModel>()
               .ReverseMap()
               .ForMember(dest => dest.User, opt => opt.Ignore())
               .ForMember(dest => dest.Post, opt => opt.Ignore())
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region friends
            CreateMap<Friends, FriendsViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Friends, SaveFriendsViewModel>()
                 .ForMember(dest => dest.Friend, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
            #endregion


        }
    }
}
