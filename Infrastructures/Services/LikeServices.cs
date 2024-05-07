using Application.Interfaces;
using Azure;
using Domain.Entity;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class LikeServices : ILikeServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LikeServices(ApplicationDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
        }

        public async Task<Like> GetUsersLike(Guid id, string u_id)
        {
            var res = await _context.Likes.FirstOrDefaultAsync(x => x.Blog == id && x.User == u_id);
            return res;

        }

        public async Task<LikeResponseVM> AddDownVote(Like like)
        {
            if (like == null || like.IsUpvote)
                return new LikeResponseVM(false, "Invalid operation: like is null or it's an upvote");

            var blogService = new BlogServices(_context,_userManager,_httpContextAccessor);
            var existingLike = await GetUsersLike(like.Blog, like.User);
            var blog = await blogService.GetBlogById(like.Blog);

            if (existingLike != null)
            {
                if (!existingLike.IsUpvote)
                {
                    _context.Likes.Remove(existingLike);
                    blog.Dislikes--;
                    blog.Popularity = (2 * blog.Likes) + (-1 * blog.Dislikes) + (1 * blog.Comment_Count);
                    _context.Blogs.Update(blog);
                    await _context.SaveChangesAsync();
                    return new LikeResponseVM(true, "Downvote removed successfully", like);
                }
                else
                {
                    return new LikeResponseVM(false, "Cannot downvote an upvoted post");
                }
            }
            else
            {
                _context.Likes.Add(like);
                blog.Dislikes++;
                blog.Popularity = (2 * blog.Likes) + (-1 * blog.Dislikes) + (1 * blog.Comment_Count);
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();
                return new LikeResponseVM(true, "Downvote added successfully", like);
            }

         
        }


        public async Task<LikeResponseVM> AddUpvote(Like like)
        {
            if (like == null || !like.IsUpvote)
                return new LikeResponseVM(false, "Invalid operation: like is null or it's a downvote");


            var blogService = new BlogServices(_context, _userManager, _httpContextAccessor);
            var existingLike = await GetUsersLike(like.Blog, like.User);
            var blog = await blogService.GetBlogById(like.Blog);

            if (existingLike != null)
            {
                if (existingLike.IsUpvote)
                {
                    _context.Likes.Remove(existingLike);
                    blog.Likes--;
                    blog.Popularity = (2 * blog.Likes) + (-1 * blog.Dislikes) + (1 * blog.Comment_Count);
                    _context.Blogs.Update(blog);
                    await _context.SaveChangesAsync();
                    return new LikeResponseVM(true, "Upvote removed successfully");
                }
                else
                {
                    return new LikeResponseVM(false, "Cannot upvote a downvoted post");
                }
            }
            else
            {
                _context.Likes.Add(like);
                blog.Likes++;
                blog.Popularity = (2 * blog.Likes) + (-1 * blog.Dislikes) + (1 * blog.Comment_Count);
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();
                return new LikeResponseVM(true, "Upvote added successfully", like);
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
