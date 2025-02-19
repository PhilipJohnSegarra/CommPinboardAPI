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
        public string? Title { get; set; }

        public string? Content { get; set; }
        
        public DateTime? EventDate { get; set; }

        public string? Location { get; set; }

        public long UserId { get; set; }

        public ICollection<Comment> Comments{ get; set; } = new List<Comment>();

        public User User { get; set; } = new User();

    }
}