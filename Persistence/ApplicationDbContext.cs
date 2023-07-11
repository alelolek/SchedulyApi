using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Event> Events { get; set; }
    }
}
