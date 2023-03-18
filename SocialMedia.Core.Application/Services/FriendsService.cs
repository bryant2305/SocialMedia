using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.ViewModels.Friends;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SocialMedia.Core.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using SocialMedia.Core.Application.ViewModels.User;
using SocialMedia.Core.Application.Helpers;

namespace SocialMedia.Core.Application.Services
{
    public class FriendsService : GenericService<SaveFriendsViewModel, FriendsViewModel, Friends>, IFriendsService
    {
        private readonly IFriendsRepository _friendRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel? userViewModel;
        private readonly IMapper _mapper;

        public FriendsService(IFriendsRepository friendRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(friendRepository, mapper)
        {
            _friendRepository = friendRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
        }

        public async Task<List<FriendsViewModel>> GetAllFriends()
        {
            var userList = await _friendRepository.GetAllAsync();
            userList = userList.Where(c => c.IdUser == userViewModel.Id).OrderByDescending(a => a.Created).ToList();

            return _mapper.Map<List<FriendsViewModel>>(userList);
        }
    }
}
