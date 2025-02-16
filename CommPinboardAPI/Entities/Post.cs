using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommPinboardAPI.Entities
{
    public class Post : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public long PostId { get; set; }

        [Required]
        public Guid UserExternalId { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Title { get; set;} = "";

        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; } = "";

        [Column(TypeName ="datetime2")]
        [AllowNull]
        public DateTime? EventDate { get; set; }

        [Column(TypeName ="nvarchar(max)")]
        [AllowNull]
        public string? Location { get; set; }
    }
}