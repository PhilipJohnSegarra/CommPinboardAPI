using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Data;
using CommPinboardAPI.Entities;
using CommPinboardAPI.Helpers.Interfaces;
using CommPinboardAPI.Repositories;

namespace CommPinboardAPI.Helpers
{
    public class PinnedPostHelper : RepositoryBase<PinnedPost>, IPinnedPostHelper
    {
        public PinnedPostHelper(DataContext db) : base(db)
        {

        }
        public async Task<List<PinnedPost>> GetAll()
        {
            List<PinnedPost> pinnedPosts = await GetAllAsync(pinnedPost => pinnedPost.IsDeleted == false);

            return pinnedPosts;
        }
        public async Task<PinnedPost> Get(Guid externalId)
        {
            PinnedPost pinnedPost = await GetAsync(pinnedPost => pinnedPost.ExternalId == externalId);
            return pinnedPost;
        }

        public async Task<PinnedPost> Add(PinnedPost payload)
        {
            if(payload == null){
                throw new BadHttpRequestException("No pinnedPost received");
            }

            await AddAsync(payload);
            return payload;
        }

        public async Task<PinnedPost> Update(Guid externalId, PinnedPost payload){
            if(payload == null){
                throw new BadHttpRequestException("No update received");
            }

            var oldPinnedPost = await GetAsync(pinnedPost => pinnedPost.ExternalId.Equals(externalId) && pinnedPost.IsDeleted.Equals(false));
            await UpdateAsync(oldPinnedPost, payload);

            return await Get(externalId);
        }

        public async Task Delete(Guid externalId)
        {
            var pinnedPost = await GetAsync(pinnedPost => pinnedPost.ExternalId.Equals(externalId) && pinnedPost.IsDeleted.Equals(false));

            PinnedPost deletedPinnedPost = pinnedPost;
            deletedPinnedPost.IsDeleted = true;

            await UpdateAsync(pinnedPost, deletedPinnedPost);
        }
    }
}