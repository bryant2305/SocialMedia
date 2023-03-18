using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Domain.Entities;
using SocialMedia.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Persistence.Repository
{
    public class PublicacionesRepository : GenericRepository<Publicaciones>, IPublicacionesRepository
    {
        private readonly ApplicationContext _dbContext;

        public PublicacionesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<List<Publicaciones>> GetAllAsync()  
        {
            return await _dbContext.Set<Publicaciones>()
                .Include(a => a.User)
                .Include(v => v.Comentarios)
                .ToListAsync(); 
        }
        public virtual async Task<Publicaciones> GetBywithRelationship(int id)
        {
            var temp = await _dbContext.Set<Publicaciones>().Where(p => p.Id == id).Include(p => p.User).ToListAsync();
            return temp.First();
        }
    }
}

