using SocialMedia.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Domain.Entities
{
    public class Comentarios : AuditableBaseEntity
    {
        public string? Descripcion { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public Publicaciones? Post { get; set; }
        public User? User { get; set; }
    }
}
