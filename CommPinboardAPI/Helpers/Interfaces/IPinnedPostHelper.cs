using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Entities;

namespace CommPinboardAPI.Helpers.Interfaces
{
    public interface IPinnedPostHelper
    {
        Task<List<PinnedPost>> GetAll();
        Task<PinnedPost> Get(Guid externalId);
        Task<PinnedPost> Add(PinnedPost payload);
        Task<PinnedPost> Update(Guid externalId, PinnedPost payload);
        Task Delete(Guid externalId);
    }
}