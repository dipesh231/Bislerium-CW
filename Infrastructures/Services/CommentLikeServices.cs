using Application.Interfaces;
using Domain.Entity;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class CommentLikeServices : ILikeCommentServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentLikeServices(ApplicationDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
        }


        public async Task<Like_Comment> GetCommentsUserLike(Guid id, string u_id)
        {
            var res = await _context.LikeComments.FirstOrDefaultAsync(x => x.Comment == id && x.User == u_id);
            return res;

        }

        public async Task<LikeCommentResponseVM> AddDownVote(Like_Comment likecmt)
        {
            if (likecmt == null || likecmt.IsUpvote)
                return new LikeCommentResponseVM(false, "Invalid operation: like is null or it's an upvote");

            var commentService = new CommentServices(_context);
            var existingLike = await GetCommentsUserLike(likecmt.Comment, likecmt.User);
            var comments = await commentService.GetCommentById(likecmt.Comment);

            if (existingLike != null)
            {
                if (!existingLike.IsUpvote)
                {
                    _context.LikeComments.Remove(existingLike);
                    comments.Dislikes--;
                    
                    _context.Comments.Update(comments);
                    await _context.SaveChangesAsync();
                    return new LikeCommentResponseVM(true, "Downvote removed successfully", likecmt);
                }
                else
                {
                    return new LikeCommentResponseVM(false, "Cannot downvote an upvoted post");
                }
            }
            else
            {
                _context.LikeComments.Add(likecmt);
                comments.Dislikes++;
           
                _context.Comments.Update(comments);
                await _context.SaveChangesAsync();
                return new LikeCommentResponseVM(true, "Downvote added successfully", likecmt) ;
            }


        }
        public async Task<LikeCommentResponseVM> AddUpvote(Like_Comment likecmt)
        {
            if (likecmt == null || !likecmt.IsUpvote)
                return new LikeCommentResponseVM(false, "Invalid operation: like is null or it's an downvote");

            var commentService = new CommentServices(_context);
            var existingLike = await GetCommentsUserLike(likecmt.Comment, likecmt.User);
            var comments = await commentService.GetCommentById(likecmt.Comment);

            if (existingLike != null)
            {
                if (existingLike.IsUpvote)
                {
                    _context.LikeComments.Remove(existingLike);
                    comments.Likes--;

                    _context.Comments.Update(comments);
                    await _context.SaveChangesAsync();
                    return new LikeCommentResponseVM(true, "Upvote removed successfully", likecmt);
                }
                else
                {
                    return new LikeCommentResponseVM(false, "Cannot downvote an upvoted post");
                }
            }
            else
            {
                _context.LikeComments.Add(likecmt);
                comments.Likes++;

                _context.Comments.Update(comments);
                await _context.SaveChangesAsync();
                return new LikeCommentResponseVM(true, "Upvote added successfully", likecmt);
            }


        }

        public async Task DeleteVote(Guid id)
        {
            var like = await _context.Likes.FirstOrDefaultAsync(x => x.Id == id);
            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }


    }
}
