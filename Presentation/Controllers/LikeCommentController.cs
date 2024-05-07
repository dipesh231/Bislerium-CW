using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeCommentController : ControllerBase
    {


        private readonly ILikeCommentServices _likeCommentService;
        public LikeCommentController(ILikeCommentServices likeCommentService)
        {
            _likeCommentService = likeCommentService;
        }

        [HttpPost, Route("UpvoteComment")]
        public async Task<IActionResult> Upvote(Like_Comment
            likecmt)
        {
            var result = await _likeCommentService.AddUpvote(likecmt);
            return Ok(result);
        }

        [HttpPost, Route("DownvoteComment")]
        public async Task<IActionResult> DownVote(Like_Comment likecmt)
        {
            var result = await _likeCommentService.AddDownVote(likecmt);
            return Ok(result);
        }

    }
}
