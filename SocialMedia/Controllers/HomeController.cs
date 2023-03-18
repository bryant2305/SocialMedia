using Microsoft.AspNetCore.Mvc;
using SocialMedia.Middlewares;
using SocialMedia.Models;
using System.Diagnostics;
using SocialMedia.Core.Application.ViewModels.Publicaciones;
using SocialMedia.Core.Application.ViewModels.User;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.ViewModels.Comentarios;
using SocialMedia.Core.Application.ViewModels.Publicaciones;
using SocialMedia.Core.Application.ViewModels.Comentarios;

namespace SocialMedia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IUserService _userService;
        private readonly IPublicacionesService _postService;
        private readonly IComentariosService _commentService;
        private readonly IHttpContextAccessor _ihttpContextAccessor;
        private readonly UserViewModel? userViewModel;

        public HomeController(ILogger<HomeController> logger, ValidateUserSession validateUserSession, IComentariosService commentService ,IUserService userService, IHttpContextAccessor ihttpContextAccessor, IPublicacionesService postService)
        {
            _validateUserSession = validateUserSession;
            _userService = userService;
            _logger = logger;
            _ihttpContextAccessor = ihttpContextAccessor;
            _postService = postService;
            _commentService = commentService;
            userViewModel = _ihttpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public  async Task<IActionResult> Index()
        {
            
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "SinPermiso" });
            }

            ViewBag.user = userViewModel;
            ViewBag.UserPost = await _postService.GetAllMyPublications();

            return View();
        }
        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View("SavePublicacion", new SavePublicacionesViewModel());

        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(SavePublicacionesViewModel spvm)
        {
            if (!ModelState.IsValid)
            {
                return View("SavePublicacion", spvm);
            }
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "SinPermiso" });
            }
            if (spvm.File != null )
            {
                spvm.ImgUrl = HelperFile.UploadFile(spvm.File, userViewModel.Id, "Posts");
            }    

            await _postService.Add(spvm);

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View("SavePublicacion", await _postService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePublicacionesViewModel spvm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SavePublicacion", spvm);
            }
            SavePublicacionesViewModel productVm = await _postService.GetByIdSaveViewModel(spvm.Id);
            spvm.ImgUrl = UploadFile(spvm.File, spvm.Id, true, productVm.ImgUrl);
            await _postService.Update(spvm, spvm.Id);

           
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _postService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _postService.Delete(id);

            //get directory path
            string basePath = $"/Images/Posts/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }



        [HttpPost]
        public async Task<IActionResult> AddComment(SavePublicacionesViewModel spvm)
        {
           
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "SinPermiso" });
            }

            if (spvm.NuevoComentario == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

           SaveComentariosViewModel savecomment = new();
            savecomment.Descripcion = spvm.NuevoComentario;
            savecomment.PostId = spvm.Idpost;
            savecomment.UserId = userViewModel.Id;


            await _commentService.Add(savecomment);


            return RedirectToRoute(new { controller = "Amigos", action = "Index" });
        }

      
        public IActionResult SinPermiso()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Posts/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}

    