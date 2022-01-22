using System.Threading.Tasks;

using Municorn.Infrastructure.Enums;

namespace Municorn.Infrastructure.Interfaces
{
    public interface INotificationSender<out T> where T: ICreateNotificationRequest
    {
        Task<NotificationStatus> SendNotificationAsync(ICreateNotificationRequest notification);
    }
}
