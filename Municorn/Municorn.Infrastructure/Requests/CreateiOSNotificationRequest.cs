using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Municorn.Infrastructure.Interfaces;

namespace Municorn.Infrastructure.Requests
{
    /// <summary>
    /// Для iOS устройств уведомление представляет из себя:
    /// </summary>
    public record CreateiOSNotificationRequest : ICreateNotificationRequest
    {
        [Required]
        [StringLength(50)]
        public string PushToken { get; init; }

        [Required]
        [StringLength(2000)]
        public string Alert { get; init; }

        public int Priority { get; init; } = 10;

        public bool IsBackground { get; init; } = true;

        [JsonIgnore]
        public string Id => $"iOS_{PushToken}";
    }
}