using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CommPinboardAPI.Entities
{
    public class Comment : BaseEntity
    {
        [Key]
        public long CommentId { get; set; }

        public long PostId { get; set; }
        public long UserId { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [Required]
        public string Content { get; set; } = "";
    }
}