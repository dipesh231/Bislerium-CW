using Application.Interfaces;
using Domain.Entity;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly ApplicationDbContext _context;
        public CommentServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CommentResponseVM> AddComment(Comment comments)
        {
            await _context.Comments.AddAsync(comments);
            await _context.SaveChangesAsync();
            var blog = await _context.Blogs.FindAsync(comments.Blog_Id);
            if (blog != null)
            {
                blog.Comment_Count++;
                blog.Popularity = (2 * blog.Likes) + (-1 * blog.Dislikes) + (1 * blog.Comment_Count);
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Handle the case where the associated blog is not found
                return new CommentResponseVM(false, "Not Found");

            }

            return new CommentResponseVM(true, "Comment Added Successfully", comments);
            
        }


        public async Task<CommentResponseVM> DeleteComment(Guid id)
        {
            var result = await _context.Comments.FindAsync(id);
           
            if (result != null)
            {
                var blog = await _context.Blogs.FindAsync(result.Blog_Id);
                if (blog != null)
                {
                    blog.Comment_Count--;
                    blog.Popularity = (2 * blog.Likes) + (-1 * blog.Dislikes) + (1 * blog.Comment_Count);
                    _context.Blogs.Update(blog);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return new CommentResponseVM(false, "Not Found");

                }
                _context.Comments.Remove(result);
                await _context.SaveChangesAsync();
                return new CommentResponseVM(true, " Comment Sucessfully Deleted");
            }
            else
            {
                return new CommentResponseVM(false, " Comment Couldn't be Deleted");
            }
        }

        public async Task<IEnumerable<Comment>> GetAllComment()
        {
            var result = await _context.Comments.ToListAsync();
            return result;
        }

        public async Task<Comment> GetCommentById(Guid id)
        {
            var result= await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

 

        public async Task<CommentResponseVM> UpdateComment(Comment comments)
        {
            Comment prevComment = await GetCommentById(comments.Id);
            Comment_History history = new Comment_History();


            if (prevComment != null)
            {
                history.Comments = prevComment.Id;
                history.CommentContentPrevious = prevComment.Content;
                history.Comment_Created_at_Time = prevComment.Posted_At;
                await _context.CommentHistories.AddAsync(history);


                prevComment.Content = comments.Content;
  


                _context.Comments.Update(prevComment);
                await _context.SaveChangesAsync();

            }
            return new CommentResponseVM(true,"Sucessfully UPDATED", prevComment);

        }

  
    }
}

