using SocialMedia.Core.Application.ViewModels.Friends;
using SocialMedia.Core.Application.ViewModels.Publicaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
 
        public string Phone { get; set; }

        public string? Photo { get; set; }

         public bool Activo { get; set; }

        public ICollection<PublicacionesViewModel> Post { get; set; }
        public ICollection<FriendsViewModel>? Friendship { get; set; }
    }
}
