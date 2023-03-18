using Microsoft.AspNetCore.Mvc;
using SocialMedia.Middlewares;
using SocialMedia.Models;
using System.Diagnostics;
using SocialMedia.Core.Application.ViewModels.Publicaciones;
using SocialMedia.Core.Application.ViewModels.User;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.ViewModels.Comentarios;
using SocialMedia.Core.Application.ViewModels.Friends;

namespace SocialMedia.Controllers
{
    public class AmigosController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IUserService _userService;
        private readonly IFriendsService _friendservice;
        private readonly IComentariosService _commentService;
        private readonly IHttpContextAccessor _ihttpContextAccessor;
        private readonly UserViewModel? userViewModel;
        private readonly IPublicacionesService _postService;


        public AmigosController(ILogger<HomeController> logger, IPublicacionesService postService, ValidateUserSession validateUserSession, IComentariosService commentService, IUserService userService, IHttpContextAccessor ihttpContextAccessor, IFriendsService friendservice)
        {
            _validateUserSession = validateUserSession;
            _userService = userService;
            _logger = logger;
            _ihttpContextAccessor = ihttpContextAccessor;
            _friendservice = friendservice;
            _commentService = commentService;
            _postService = postService;
            userViewModel = _ihttpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<IActionResult> Index()
        {

            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "SinPermiso" });
            }

            ViewBag.user = userViewModel;
            
            var FA = await _friendservice.GetAllFriends();

            if (FA.Count() > 0)
            {
                var Publications = await _postService.GetAllMyAmigosPost(FA.First().IdFriend);
                for (int i = 1; i < FA.Count(); i++)
                {
                    var T = await _postService.GetAllMyAmigosPost(FA[i].IdFriend);
                    Publications.AddRange(T.ToList());
                }
                HashSet<string> nombres = new HashSet<string>();

                foreach (var i in Publications)
                {
                    nombres.Add(i.User.Name + " "+i.User.Username);
                }

                ViewBag.amigos = nombres.ToList();
                ViewBag.friendpost = Publications;
            }
            return View();
        }

        public async Task<IActionResult> AddFriend()
        {

            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "SinPermiso" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> agregar(SaveFriendsViewModel sfvm)
        {

            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "SinPermiso" });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "User", action = "agregar" });

            }
            UserViewModel existe  = await _userService.GetByusernameViewModel(sfvm.Friend);

            if (existe == null)
            {
                ModelState.AddModelError("UserValidation", "El usuario no existe");
                return View("AddFriend", null);
            }

            sfvm.IdFriend = existe.Id;
            sfvm.IdUser = userViewModel.Id;
            await _friendservice.Add(sfvm);

            int temp = sfvm.IdFriend;
            sfvm.IdFriend = sfvm.IdUser;
            sfvm.IdUser = temp;
            await _friendservice.Add(sfvm);


            return RedirectToRoute(new { controller = "Amigos", action = "Index" });

        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _friendservice.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _friendservice.Delete(id);
            return RedirectToRoute(new { controller = "Amigos", action = "Index" });
        }

    }
}

    

