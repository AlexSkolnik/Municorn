using Municorn.Infrastructure.Enums;

namespace Municorn.Infrastructure.Responses
{
    public record CreateNotificationResponse
    {
        public string Id { get; init; }
        public NotificationStatus Status { get; init; }

        public CreateNotificationResponse(string id, NotificationStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}
