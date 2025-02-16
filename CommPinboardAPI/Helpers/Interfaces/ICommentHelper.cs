using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Entities;

namespace CommPinboardAPI.Helpers.Interfaces
{
    public interface ICommentHelper
    {
        Task<List<Comment>> GetAll();
        Task<Comment> Get(Guid externalId);
        Task<Comment> Add(Comment payload);
        Task<Comment> Update(Guid externalId, Comment payload);
        Task Delete(Guid externalId);
    }
}