using Microsoft.EntityFrameworkCore;

using Municorn.DAL.Entities;

namespace Municorn.DAL.Contexts
{
    public abstract class MunicornBaseDbContext : DbContext, IMunicornDbContext
    {
        public DbSet<Notification> Notifications { get; set; }

        protected MunicornBaseDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
