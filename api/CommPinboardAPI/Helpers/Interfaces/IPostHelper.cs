using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Entities;

namespace CommPinboardAPI.Helpers.Interfaces
{
    public interface IPostHelper
    {
        Task<List<Post>> GetAll();
        Task<Post> Get(Guid externalId);
        Task<Post> Add(Post payload);
        Task<Post> Update(Guid externalId, Post payload);
        Task Delete(Guid externalId);
    }
}