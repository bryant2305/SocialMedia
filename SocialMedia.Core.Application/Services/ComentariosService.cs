using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.ViewModels.Comentarios;
using SocialMedia.Core.Application.ViewModels.User;
using SocialMedia.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Services
{
    public class ComentariosService : GenericService<SaveComentariosViewModel, ComentariosViewModel, Comentarios>,  IComentariosService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel? userViewModel;
        private readonly IMapper _mapper;
        private readonly IComentariosRepository _commentRepository;

        public ComentariosService(IHttpContextAccessor httpContextAccessor, IMapper mapper, IComentariosRepository commentRepository) : base(commentRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public override async Task<SaveComentariosViewModel> Add(SaveComentariosViewModel vm)
        {
            vm.UserId = userViewModel.Id;

            return await base.Add(vm);
        }

        public override async Task Update(SaveComentariosViewModel vm, int id)
        {
            vm.UserId = userViewModel.Id;

            await base.Update(vm,id);
        }


    }
       
    }
    
