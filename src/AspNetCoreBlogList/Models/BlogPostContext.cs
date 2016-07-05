using Microsoft.EntityFrameworkCore;

namespace AspNetCoreBlogList.Models
{
    public class BlogPostContext : DbContext
    {
        public BlogPostContext (DbContextOptions<BlogPostContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}
