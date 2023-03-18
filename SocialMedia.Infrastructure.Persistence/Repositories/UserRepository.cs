using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Application.ViewModels.User;
using SocialMedia.Core.Domain.Entities;
using SocialMedia.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryptation.ComputeSha256Hash(entity.Password);
            await base.AddAsync(entity);
            return entity;
        }

        public async Task<User> GetByUsernameAsync(string? username)
        {
            var temp = await _dbContext.Set<User>().Where(u => u.Username == username).ToListAsync();
            return temp.First();
        }

        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordEncryptation.ComputeSha256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>().FirstOrDefaultAsync(user=> user.Username == loginVm.Username && user.Password == passwordEncrypt);
            return user;
        }
       
    }
}
