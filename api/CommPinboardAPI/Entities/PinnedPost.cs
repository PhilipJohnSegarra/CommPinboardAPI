using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommPinboardAPI.Entities
{
    public class PinnedPost : BaseEntity
    {
        [Key]
        public long PinnedPostId { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
    }
}