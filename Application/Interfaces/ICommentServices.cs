using Domain.Entity;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICommentServices
    {
        Task<CommentResponseVM> AddComment(Comment comments);
        Task<CommentResponseVM> UpdateComment(Comment comments);
        Task<CommentResponseVM> DeleteComment(Guid id);
        Task<Comment> GetCommentById(Guid id);
        Task<IEnumerable<Comment>> GetAllComment();
    }
}
