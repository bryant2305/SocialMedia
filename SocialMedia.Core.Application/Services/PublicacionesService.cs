using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.ViewModels.Publicaciones;
using SocialMedia.Core.Application.ViewModels.User;
using SocialMedia.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Services
{
    public class PublicacionesService : GenericService<SavePublicacionesViewModel, PublicacionesViewModel, Publicaciones>,IPublicacionesService
    {
        private readonly IPublicacionesRepository _publicacionesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel? userViewModel;
        private readonly IMapper _mapper;

        public PublicacionesService(IPublicacionesRepository publicacionesRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(publicacionesRepository, mapper)
        {
            _publicacionesRepository = publicacionesRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
        }

        public override async Task<SavePublicacionesViewModel> Add(SavePublicacionesViewModel vm)
        {
            vm.UserId = userViewModel.Id;

            return await base.Add(vm);
        }

        public override async Task Update(SavePublicacionesViewModel vm, int id)
        {
            vm.UserId = userViewModel.Id;

            await base.Update(vm, id);
        }
        public async Task<List<PublicacionesViewModel>> GetAllMyPublications()
        {
            var mylist = await _publicacionesRepository.GetAllAsync();

            mylist = mylist.Where(p => p.UserId == userViewModel.Id).OrderByDescending(p => p.Created).ToList(); 

            return _mapper.Map<List<PublicacionesViewModel>>(mylist);
        }
        Task<PublicacionesViewModel> IPublicacionesService.GetPostsandDetails(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<PublicacionesViewModel>> GetAllMyAmigosPost()
        {
            var mylist = await _publicacionesRepository.GetAllAsync();

            mylist = mylist.Where(p => p.UserId != userViewModel.Id).OrderByDescending(p => p.Created).ToList();
            
            return _mapper.Map<List<PublicacionesViewModel>>(mylist);
        }

        public async Task<List<PublicacionesViewModel>> GetAllMyAmigosPost(int friendid)
        {
            var mylist = await _publicacionesRepository.GetAllAsync();

            mylist = mylist.Where(p => p.UserId == friendid).OrderByDescending(p => p.Created).ToList();

            return _mapper.Map<List<PublicacionesViewModel>>(mylist);
        }

    }
}
