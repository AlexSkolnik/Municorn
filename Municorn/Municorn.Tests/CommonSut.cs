using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;

using Municorn.DAL.Contexts;
using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Repositories;

namespace Municorn.Tests
{
    public static class CommonSut
    {
        public static INotificationRepository GetInMemoryNotificationRepository()
        {
            DbContextOptions<MunicornDbContext> options;
            var builder = new DbContextOptionsBuilder<MunicornDbContext>();
            builder.UseInMemoryDatabase("Municorn");
            options = builder.Options;

            var personDataContext = new MunicornDbContext(options);
            personDataContext.Database.EnsureDeleted();
            personDataContext.Database.EnsureCreated();
            return new NotificationRepository(new Mock<ILogger<NotificationRepository>>().Object, personDataContext);
        }
    }
}
