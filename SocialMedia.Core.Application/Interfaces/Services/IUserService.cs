using SocialMedia.Core.Application.ViewModels.Friends;
using SocialMedia.Core.Application.ViewModels.User;
using SocialMedia.Core.Domain.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel,User>
    {
        Task<UserViewModel> Login(LoginViewModel vm);
        Task<UserViewModel> GetByIdViewModel(int id);
        Task<UserViewModel> Olvidepass(OlvideContraViewModel fm);
        Task<UserViewModel> GetByusernameViewModel(String fm);
  
    }
}
