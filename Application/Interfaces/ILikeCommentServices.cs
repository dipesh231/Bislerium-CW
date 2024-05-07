using Domain.Entity;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILikeCommentServices
    {
        Task<CommentResponseVM> AddUpvote(Like_Comment likecmt);
        Task<CommentResponseVM> AddDownVote(Like_Comment likecmt);
        Task<Like_Comment> GetCommentsUserLike(Guid id,string u_id);
        Task DeleteVote(Guid id);
    }
}
