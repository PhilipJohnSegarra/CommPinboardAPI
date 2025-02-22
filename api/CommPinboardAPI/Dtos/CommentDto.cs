using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommPinboardAPI.Dtos
{
    public class CommentDto
    {
        public Guid ExternalId { get; set; } = Guid.NewGuid();
        public long CommentId { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; } = "";
        
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DateUpdated { get; set;} = DateTime.UtcNow;
    }
}