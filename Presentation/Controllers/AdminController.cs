﻿using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminService;
        public AdminController(IAdminServices adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("all-time-blog-post-count")]
        public async Task<IActionResult> GetAllTimeBlogPostCount()
        {
            try
            {
                var count = await _adminService.GetAllTimeBlogPostCount();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("all-time-comment-count")]
        public async Task<IActionResult> GetAllTimeCommentCount()
        {
            try
            {
                var count = await _adminService.GetAllTimeCommentCount();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("all-time-downvotes-count")]
        public async Task<IActionResult> GetAllTimeDownvoteCount()
        {
            var count = await _adminService.GetAllTimeDownvoteCount();
            return Ok(count);
        }

        [HttpGet("all-time-upvotes-count")]
        public async Task<IActionResult> GetAllTimeUpvoteCount()
        {
            var count = await _adminService.GetAllTimeUpvoteCount();
            return Ok(count);
        }

        [HttpGet("daily-blog-posts-count")]
        public async Task<IActionResult> GetDailyBlogPostCount([FromQuery] DateTime date)
        {
            var count = await _adminService.GetDailyBlogPostCount(date);
            return Ok(count);
        }

        [HttpGet("daily-comments-count")]
        public async Task<IActionResult> GetDailyCommentCount([FromQuery] DateTime date)
        {
            var count = await _adminService.GetDailyCommentCount(date);
            return Ok(count);
        }

        [HttpGet("daily-downvotes")]
        public async Task<IActionResult> GetDailyDownvoteCount([FromQuery] DateTime date)
        {
            var count = await _adminService.GetDailyDownvoteCount(date);
            return Ok(count);
        }

        [HttpGet("daily-upvotes-count")]
        public async Task<IActionResult> GetDailyUpvoteCount([FromQuery] DateTime date)
        {
            var count = await _adminService.GetDailyUpvoteCount(date);
            return Ok(count);
        }

        [HttpGet("daily-activity")]
        public async Task<IActionResult> GetDailyActivity([FromQuery] DateTime date)
        {
            var dailyActivity = new
            {
                Date = date,
                BlogPosts = await _adminService.GetDailyBlogPostCount(date),
                Comments = await _adminService.GetDailyCommentCount(date),
                Downvotes = await _adminService.GetDailyDownvoteCount(date),
                Upvotes = await _adminService.GetDailyUpvoteCount(date)
            };
            return Ok(dailyActivity);
        }

        [HttpGet("top-10-popular-posts")]
        public async Task<IActionResult> GetTop10PopularPosts()
        {
            try
            {
                var popularPosts = await _adminService.GetTop10PopularPosts();
                return Ok(popularPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("top-10-popular-posts-by-month")]
        public async Task<IActionResult> GetTop10PopularPostByMonth([FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                var popularBloggers = await _adminService.GetTop10PopularPostsByMonth(month, year);
                return Ok(popularBloggers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("top-10-popular-bloggers")]
        public async Task<IActionResult> GetTop10PopularBloggers()
        {
            try
            {
                var popularBloggers = await _adminService.GetTop10PopularBloggers();
                return Ok(popularBloggers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("top-10-popular-bloggers-by-month")]
        public async Task<IActionResult> GetTop10PopularBloggersByMonth([FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                var popularBloggers = await _adminService.GetTop10PopularBloggersByMonth(month, year);
                return Ok(popularBloggers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
