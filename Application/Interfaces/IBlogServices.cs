﻿using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlogServices
    {
        Task<Blog> AddBlog(Blog blogs);
        Task<Blog> UpdateBlog(Blog blogs);
        Task DeleteBlog(Guid id);
        Task<Blog> GetBlogById(Guid id);
        Task<IEnumerable<Blog>> GetAllBlogs();

        Task<IEnumerable<Blog>> GetAllBlogsPagination(string sortField, int pageNumber = 1, int pageSize = 5);


    }
}
