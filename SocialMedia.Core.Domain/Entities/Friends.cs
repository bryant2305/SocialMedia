using SocialMedia.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Domain.Entities
{
    public  class Friends : AuditableBaseEntity
    {
        public int IdUser { get; set; }
        public int IdFriend { get; set; }
        public User? User { get; set; }

    }
}
