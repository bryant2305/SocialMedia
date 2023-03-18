using SocialMedia.Core.Application.ViewModels.Friends;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Interfaces.Repositories
{
    public interface IFriendsRepository : IGenericRepository<Friends>
    {
        Task<List<Friends>> GetBywithRelationship(int userid);
      
    }
}
