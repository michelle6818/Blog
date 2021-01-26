using System;
using System.Collections.Generic;
using System.Text;
using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<CategoryPost> CategoryPost { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Tag> Tag { get; set; }
    }
}
