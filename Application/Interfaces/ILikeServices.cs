using Domain.Entity;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILikeServices
    {
        Task<LikeResponseVM> AddUpvote(Like like);
        Task<LikeResponseVM> AddDownVote( Like like);
        Task<Like> GetUsersLike(Guid id, string u_id);
        Task DeleteVote(Guid id);

    }
}
