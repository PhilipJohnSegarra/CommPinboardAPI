using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Entities;

namespace CommPinboardAPI.Dtos
{
    public class PostDto
    {
        public Guid ExternalId { get; set; } = Guid.NewGuid();
        public long PostId { get; set; }
        public string? Title { get; set; }

        public string? Content { get; set; }
        
        public DateTime? EventDate { get; set; }

        public string? Location { get; set; }

        public long UserId { get; set; }

        public UsersDto? User { get; set; }

    }
}