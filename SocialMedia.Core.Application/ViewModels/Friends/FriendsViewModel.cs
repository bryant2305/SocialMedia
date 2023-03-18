using SocialMedia.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.Friends
{
    public class FriendsViewModel
    {
        public int IdUser { get; set; }
        public int IdFriend { get; set; }
        public UserViewModel? User { get; set; }
    }
}
