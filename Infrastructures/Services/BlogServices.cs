﻿using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class BlogServices : IBlogServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogServices(ApplicationDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
        }
        public async Task<Blog> AddBlog(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task DeleteBlog(Guid id)
        {
            var result = await _context.Blogs.FindAsync(id);
            if (result != null)
            {
                // Get the current user's ID
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                string user = await _userManager.FindByIdAsync(userId);
              
                if (result.User != user)
                {
                    throw new UnauthorizedAccessException("Not authorized to delete this blog");
                }
                _context.Blogs.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllBlogs()
        {
            var result = await _context.Blogs.ToListAsync();
            return result;
        }

        public async Task<Blog> GetBlogById(Guid id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);

        }

   

        public async Task<Blog> UpdateBlog(Blog blog)
        {
            Blog prevBlog = await GetBlogById(blog.Id);
            BlogHistory history = new BlogHistory();


            if (prevBlog != null)
            {
                history.Blog = prevBlog.Id;
                history.BlogContentPrevious = prevBlog.Content;
                history.BlogTitlePrevious = prevBlog.Title;
                history.BlogCreated_atTime = prevBlog.Created_at;
                history.BlogImagePrevious = prevBlog.Image;
                await _context.BlogsHistories.AddAsync(history);

                if (!string.IsNullOrEmpty(blog.Image))
                {
                    prevBlog.Image = blog.Image;

                }
                prevBlog.Title = blog.Title;
                prevBlog.Content = blog.Content;
              

                _context.Blogs.Update(prevBlog);
                await _context.SaveChangesAsync();

            }
            return prevBlog;

        }
    }
}