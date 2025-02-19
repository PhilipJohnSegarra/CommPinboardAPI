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
    public class User : BaseEntity
    {
        [Key]
        public long UserId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string UserName { get; set; } = "";

        [Column(TypeName = "nvarchar(150)")]
        [AllowNull]
        public string? FullName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        [Required]
        public string Email { get; set; } = "";

        [Column(TypeName = "nvarchar(max)")]
        [Required]
        public string PasswordHash { get; set; } = "";

        
        public ICollection<Comment> Comments{ get; set; } = new List<Comment>();
        public ICollection<Post> Posts{ get; set; } = new List<Post>();
        public ICollection<PinnedPost> PinnedPosts{ get; set; }= new List<PinnedPost>();
    }
}