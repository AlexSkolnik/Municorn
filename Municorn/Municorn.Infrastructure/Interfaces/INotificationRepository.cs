using System.Threading.Tasks;

using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Models;

namespace Municorn.Infrastructure.Interfaces
{
    public interface INotificationRepository
    {
        Task<NotificationStatus> GetNotificationStatusByIdAsync(string id);
        Task<string> SaveNotificationAsync(NotificationDto data);
        Task UpdateNotificationStatusAsync(NotificationDto dto);
    }
}