using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Data;
using CommPinboardAPI.Entities;
using CommPinboardAPI.Helpers.Interfaces;
using CommPinboardAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CommPinboardAPI.Helpers
{
    public class CommentHelper : RepositoryBase<Comment>, ICommentHelper
    {
        IPostHelper _postHelper;
        DataContext _db;
        public CommentHelper(DataContext db, IPostHelper postHelper) : base(db)
        {
            _postHelper = postHelper;
            _db = db;
        }
        public async Task<List<Comment>> GetAll()
        {
            List<Comment> comments = await GetAllAsync(comment => comment.IsDeleted == false);

            return comments;
        }
        public async Task<Comment> Get(Guid externalId)
        {
            Comment comment = await GetAsync(comment => comment.ExternalId == externalId);
            return comment;
        }

        public async Task<Comment> Add(Comment payload)
        {
            if(payload == null){
                throw new BadHttpRequestException("No comment received");
            }

            await AddAsync(payload);
            return payload;
        }

        public async Task<Comment> Update(Guid externalId, Comment payload){
            if(payload == null){
                throw new BadHttpRequestException("No update received");
            }

            var oldComment = await GetAsync(comment => comment.ExternalId.Equals(externalId) && comment.IsDeleted.Equals(false));
            await UpdateAsync(oldComment, payload);

            return await Get(externalId);
        }

        public async Task Delete(Guid externalId)
        {
            var comment = await GetAsync(comment => comment.ExternalId.Equals(externalId) && comment.IsDeleted.Equals(false));

            Comment deletedComment = comment;
            deletedComment.IsDeleted = true;

            await UpdateAsync(comment, deletedComment);
        }

        public async Task<List<Comment>> GetPostComments(Guid externalId)
        {
            var postComments = _db.Posts.Where(p => p.ExternalId == externalId)
            .Include(p => p.Comments)
            .Select(p => new {p.Comments});

            return (List<Comment>)postComments;
        }
    }
}