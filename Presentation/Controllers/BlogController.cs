﻿using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private readonly IBlogServices _blogService;

        public BlogController(IBlogServices blogService)
        {
            _blogService = blogService;
        }

        [HttpPost, Route("AddBlog")]
        public async Task<IActionResult> AddBlog(Blog std)
        {
            var result = await _blogService.AddBlog(std);
            return Ok(result);
        }

        [HttpGet, Route("GetBlog")]
        public async Task<IActionResult> GetABlog(Guid id)
        {
            var result = await _blogService.GetBlogById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet, Route("GetBlogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var result = await _blogService.GetAllBlogs();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetBlogspag")]
        public async Task<ActionResult<IEnumerable<Blog>>> GetAllBlogsPagination(string sortField, int pageNumber, int pageSize)
        {

            var result = await _blogService.GetAllBlogsPagination(sortField, pageNumber, pageSize);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }

        [HttpDelete, Route("DeleteBlog")]
        public async Task<IActionResult> DeleteBlog(Guid Id)
        {
            await _blogService.DeleteBlog(Id);

            return Ok();
        }

        [HttpPut, Route("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog(Blog blog)
        {
            var result = await _blogService.UpdateBlog(blog);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
