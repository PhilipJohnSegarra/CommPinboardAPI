using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommPinboardAPI.Entities
{
    public class Comment : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public long CommentId { get; set; }

        public Guid PostExternalId { get; set; }
        public Guid UserExternalId { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [Required]
        public string Content { get; set; } = "";
    }
}