using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Entities;

namespace CommPinboardAPI.Dtos
{
    public class UsersDto
    {
        public Guid ExternalId { get; set; } = Guid.NewGuid();
        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public ICollection<Comment> Comments{ get; set; } = new List<Comment>();
        public ICollection<Post> Posts{ get; set; } = new List<Post>();
        public ICollection<PinnedPost> PinnedPosts{ get; set; }= new List<PinnedPost>();

        
    }
}