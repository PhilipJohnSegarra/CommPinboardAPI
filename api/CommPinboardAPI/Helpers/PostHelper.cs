using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Data;
using CommPinboardAPI.Dtos;
using CommPinboardAPI.Entities;
using CommPinboardAPI.Helpers.Interfaces;
using CommPinboardAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CommPinboardAPI.Helpers
{
    public class PostHelper : RepositoryBase<Post>, IPostHelper
    {
        DataContext _db;
        public PostHelper(DataContext db) : base(db)
        {
            _db = db;
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
        
        public async Task<List<PostDto>> GetPostsWithUsers(long userExternalId){
            var pinnedPosts = _db.PinnedPosts.Where(p => p.UserId.Equals(userExternalId) && p.IsDeleted.Equals(false))
                .Select(p => p.PostId);
            var PostWithUsers = await _db.Posts
                .Where(p => p.IsDeleted.Equals(false) && !pinnedPosts.Contains(p.PostId))
                .Include(p => p.User)
                .Select(p => new PostDto
                {
                    ExternalId = p.ExternalId,
                    PostId = p.PostId,
                    Title = p.Title,
                    Content = p.Content,
                    EventDate = p.EventDate,
                    Location = p.Location,
                    UserId = p.UserId,
                    DateCreated = p.DateCreated,
                    DateUpdated = p.DateUpdated,
                    User = new UsersDto
                    {
                        ExternalId = p.User.ExternalId,
                        FullName = p.User.FullName,
                        Email = p.User.Email,
                        UserName = p.User.UserName,
                        UserId = p.User.UserId,
                        DateCreated = p.User.DateCreated,
                        DateUpdated = p.User.DateUpdated
                    }
                })
                .ToListAsync();

            return PostWithUsers;
        }
    }
}