﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Infrastructures
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-UO95FGF\\SQLEXPRESS;Database=BisleriumCW;Trusted_Connection=True;TrustServerCertificate=True");

        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<BlogHistory> BlogsHistories { get; set; }

        public DbSet<Comment_History> CommentHistories { get; set; }
        public DbSet<Like_Comment> LikeComments { get; set; }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }


    }
}