using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.ViewModels.Friends
{
    public class SaveFriendsViewModel
    {

        [Required(ErrorMessage = "complete el campo")]
        [DataType(DataType.Text)]
        public string Friend { get; set; }
        public int IdUser { get; set; }
        public int IdFriend { get; set; }

    }
}
