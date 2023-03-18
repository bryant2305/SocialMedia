using AutoMapper;
using SocialMedia.Core.Application.Dtos.Email;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.ViewModels.Friends;
using SocialMedia.Core.Application.ViewModels.User;
using SocialMedia.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SocialMedia.Core.Application.Services
{
    public class UserService : GenericService<SaveUserViewModel,UserViewModel,User>,IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailservice;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IEmailService emailservice ,IMapper mapper):base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _emailservice = emailservice;
            _mapper =  mapper;
        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            UserViewModel userVm = new();
            User user = await _userRepository.LoginAsync(vm);

            if(user == null)
            {
                return null;
            }
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> GetByIdViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<UserViewModel>(user); 
        }

        public async Task<UserViewModel> Olvidepass(OlvideContraViewModel fm)
        {
            User user = await _userRepository.GetByUsernameAsync(fm.Username);

            UserViewModel vm = _mapper.Map<UserViewModel>(user);
            String Pass = RandomPW.Generate(10);

            user.Password = PasswordEncryptation.ComputeSha256Hash(Pass);
            _userRepository.UpdateAsync(user,user.Id);
            await _emailservice.SendAsync(new EmailRequest
            {
                To = user.Email,
                From = "lacuentaprueba0910@gmail.com",
                Subject = "Reestablecer contraseña",
                Body = $"<h1>Tienes contraseña nueva!</h1> <p> es: {Pass} </p>"

            });
            return vm;

        }
        public override async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            SaveUserViewModel suvm = await base.Add(vm);
            
            await _emailservice.SendAsync(new EmailRequest
            {
                To = suvm.Email,
                From = "lacuentaprueba0910@gmail.com",
                Subject = "Activacion de Usuario",
                Body = $"<h1>Hola {suvm.Username} </h1>" +
                $"<a href='https://localhost:7153/User/Activo/{suvm.Id}' >Activa tu cuenta aqui <button  </button> </a> "

            });

            return suvm;
        }

        public async Task<UserViewModel> GetByusernameViewModel(string fm)
        {
            User user = await _userRepository.GetByUsernameAsync(fm);

            UserViewModel vm = _mapper.Map<UserViewModel>(user);
            return vm;
        }
       
    }
}
