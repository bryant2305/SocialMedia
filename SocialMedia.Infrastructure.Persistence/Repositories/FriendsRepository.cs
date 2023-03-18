using SocialMedia.Infrastructure.Persistence.Repository;
using System;
using SocialMedia.Core.Application.Interfaces.Repositories;
using System.Collections.Generic;
using SocialMedia.Core.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Infrastructure.Persistence.Contexts;

namespace SocialMedia.Infrastucture.Persistence.Repositories
{
    public class FriendsRepository : GenericRepository<Friends>, IFriendsRepository
    {
        private readonly ApplicationContext _dbContext;

        public FriendsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Friends>> GetBywithRelationship(int userid)
        {
            throw new NotImplementedException();
        }
    }
}
