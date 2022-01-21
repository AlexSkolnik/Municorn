using System;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Municorn.DAL.Contexts;
using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Mappers;
using Municorn.Infrastructure.Models;

namespace Municorn.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ILogger<NotificationRepository> _logger;
        private readonly MunicornBaseDbContext _dbContext;

        public NotificationRepository(
            ILogger<NotificationRepository> logger,
            IMunicornDbContext notificationDbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = (notificationDbContext as MunicornBaseDbContext) ?? throw new ArgumentNullException(nameof(notificationDbContext));
        }

        public async Task<NotificationStatus> GetNotificationStatusByIdAsync(string id)
        {
            try
            {
                var notification = await _dbContext.Notifications.FirstAsync(x => x.Id == id);
                var result = NotificationMapper.CreateDto(notification);
                _logger.LogInformation($"Найдено уведомление id = '{notification.Id}'. /n {JsonSerializer.Serialize(result)}");

                return result.Status;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Ошибка поиска в БД уведомления");
                throw;
            }
        }

        public async Task<string> SaveNotificationAsync(NotificationDto data)
        {
            try
            {
                var notification = NotificationMapper.CreateEntity(data);
                await _dbContext.Notifications.AddAsync(notification);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Создано новое уведомление id = '{notification.Id}'. /n {JsonSerializer.Serialize(notification)}");

                return notification.Id;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка сохранения уведомления в БД");
                throw new InvalidOperationException("Ошибка сохранения уведомления в БД", ex);
            }
        }

        public async Task UpdateNotificationStatusAsync(NotificationDto dto)
        {
            try
            {
                var notification = await _dbContext.Notifications.FirstAsync(x => x.Id == dto.Id);
                NotificationMapper.FromDtoToEntity(dto, ref notification);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Обновлено уведомление id = '{notification.Id}'.");

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка обновления уведомления в БД");
                throw new InvalidOperationException("Ошибка обновления уведомления в БД", ex);
            }
        }
    }
}
