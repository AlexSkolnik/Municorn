using Microsoft.EntityFrameworkCore;

namespace Municorn.DAL.Contexts
{
    public class MunicornDbContext : MunicornBaseDbContext
    {
        public MunicornDbContext(DbContextOptions<MunicornDbContext> options) : base(options)
        {
        }
    }
}
