using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommPinboardAPI.Dtos
{
    public class CommentDto
    {
        public Guid ExternalId { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; } = "";
    }
}