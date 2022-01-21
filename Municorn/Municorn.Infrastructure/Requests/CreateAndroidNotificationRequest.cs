using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Municorn.Infrastructure.Interfaces;

namespace Municorn.Infrastructure.Requests
{
    /// <summary>
    /// Для Android устройств уведомление представляет из себя:
    /// </summary>
    public record CreateAndroidNotificationRequest: ICreateNotificationRequest
    {
        [Required]
        [StringLength(50)]
        public string DeviceToken { get; init; }

        [Required]
        [StringLength(2000)]
        public string Message { get; init; }

        [Required]
        [StringLength(255)]
        public string Title { get; init; }

        [StringLength(2000)]
        public string? Condition { get; init; }

        [JsonIgnore]
        public string Id => $"android_{DeviceToken}";
    }
}
