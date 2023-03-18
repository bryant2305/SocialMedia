using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Domain.Entities;
using SocialMedia.Infrastructure.Persistence.Contexts;
using SocialMedia.Infrastructure.Persistence.Repository;

namespace SocialMedia.Infrastucture.Persistence.Repositories
{
    public class ComentariosRepository : GenericRepository<Comentarios>, IComentariosRepository
    {
        private readonly ApplicationContext _dbContext;
        public ComentariosRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
