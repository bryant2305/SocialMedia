using SocialMedia.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Domain.Entities
{
    public class User : AuditableBaseEntity
    {        
        public string? Username { get; set; }
        
        public string? Password { get; set; }
     
        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }
     
        public string? Photo { get; set; }
        public string? Phone { get; set; }


        public bool Activo { get; set; }

        public ICollection<Publicaciones>? Publicaciones { get; set; }
        public ICollection<Friends>? Friends { get; set; }

    }
}
