using SocialMedia.Core.Application.ViewModels.Friends;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Interfaces.Services
{
    public interface IFriendsService : IGenericService<SaveFriendsViewModel, FriendsViewModel,Friends>
    {

        Task<List<FriendsViewModel>> GetAllFriends();

    }
}
