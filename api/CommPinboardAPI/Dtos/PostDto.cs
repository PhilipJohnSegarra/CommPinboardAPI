using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommPinboardAPI.Dtos
{
    public class PostDto
    {
        public Guid ExternalId { get; set; }
        public string? Title { get; set; }

        public string? Content { get; set; }
        
        public DateTime? EventDate { get; set; }

        public string? Location { get; set; }

    }
}