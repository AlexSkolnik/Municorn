using Municorn.Infrastructure.Enums;

namespace Municorn.Infrastructure.Requests
{
    public record CreateCommonNotificationRequest
    {
        public DeviceType DeviceType { get; init; }

        public object JsonObject { get; init; }
    }
}
