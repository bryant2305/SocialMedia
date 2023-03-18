using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Middlewares;
using AutoMapper;

namespace SocialMedia.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ValidateUserSession _validateUserSession;

        public UserController(IUserService userService, ValidateUserSession validateUserSession, IMapper mapper)
        {
            _userService = userService;
            _mapper=mapper;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            
            UserViewModel userVm = await _userService.Login(vm);

            if (userVm == null)
            {
                ModelState.AddModelError("userValidation", "El usuario o la contraseña son incorrecta");
                return View(vm);
            }

            if (userVm.Activo == false)
            {
                ModelState.AddModelError("userValidation", "El usuario no esta activo");
                return View("Index");
            }

            if (userVm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(vm);
        }
        public async Task<IActionResult> Activo(int id)
        {
            SaveUserViewModel UserActivo = _mapper.Map<SaveUserViewModel>(await _userService.GetByIdViewModel(id));
            UserActivo.Activo = true;
            await _userService.Update(UserActivo ,UserActivo.Id);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            try
            {   
                SaveUserViewModel P = await _userService.Add(vm);

                if (P.Id != 0 && P != null)
                {
                    P.Photo = HelperFile.UploadFile(vm.File, P.Id,"Users");
                    await _userService.Update(P, P.Id);
                }
                }
            catch
            {
                ModelState.AddModelError("Username", "Este usuario ya existe");
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Olvide()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(new OlvideContraViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Olvide(OlvideContraViewModel fm)
        {
            if (!ModelState.IsValid)
            {
                return View(fm);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            try
            {
               
            UserViewModel uvm = await _userService.Olvidepass(fm);
            if (uvm == null)
            {
             ModelState.AddModelError("Username", "El usuario no existe.");
              return View(fm);
              }   
          }
          catch
            {
                ModelState.AddModelError("Username", "El usuario no existe.");
                return View(fm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        public IActionResult SinPermiso()
        {
            return View();
        }
    }
}
