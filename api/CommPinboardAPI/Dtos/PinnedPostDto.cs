using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Entities;

namespace CommPinboardAPI.Dtos
{
    public class PinnedPostDto
    {
        public Guid ExternalId { get; set; } = Guid.NewGuid();
        public long PinnedPostId { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
        public PostDto? Post { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set;}
        
    }
}