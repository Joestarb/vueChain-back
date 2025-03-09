using Microsoft.EntityFrameworkCore;
using vueChain.Models;

namespace vueChain.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
    }
}