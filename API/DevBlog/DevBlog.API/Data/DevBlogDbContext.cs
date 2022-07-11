using DevBlog.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.API.Data
{
    public class DevBlogDbContext : DbContext
    {
        public DevBlogDbContext(DbContextOptions options) : base(options)
        {
        }

        // DBSet
        public DbSet<Post> Posts { get; set; }
    }
}
