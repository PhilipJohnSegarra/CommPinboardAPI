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
    public class PostHelper : RepositoryBase<Post>, IPostHelper
    {
        public PostHelper(DataContext db) : base(db)
        {

        }

        public async Task<List<Post>> GetAll()
        {
            List<Post> posts = await GetAllAsync(post => post.IsDeleted == false);

            return posts;
        }
        public async Task<Post> Get(Guid externalId)
        {
            Post post = await GetAsync(post => post.ExternalId == externalId && post.IsDeleted.Equals(false));
            return post;
        }

        public async Task<Post> Add(Post payload)
        {
            if(payload == null){
                throw new BadHttpRequestException("No post received");
            }

            await AddAsync(payload);
            return payload;
        }

        public async Task<Post> Update(Guid externalId, Post payload){
            if(payload == null){
                throw new BadHttpRequestException("No update received");
            }

            var oldPost = await GetAsync(post => post.ExternalId.Equals(externalId) && post.IsDeleted.Equals(false));
            await UpdateAsync(oldPost, payload);

            return await Get(externalId);
        }

        public async Task Delete(Guid externalId)
        {
            var post = await GetAsync(post => post.ExternalId.Equals(externalId) && post.IsDeleted.Equals(false));

            Post deletedPost = post;
            deletedPost.IsDeleted = true;

            await UpdateAsync(post, deletedPost);
        }
    }
}