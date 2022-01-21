using Microsoft.EntityFrameworkCore;

using Municorn.DAL.Entities;

namespace Municorn.DAL.Contexts
{
    public interface IMunicornDbContext
    {
        DbSet<Notification> Notifications { get; set; }
    }
}
