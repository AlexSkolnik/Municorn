using System.ComponentModel.DataAnnotations;

using Municorn.Infrastructure.Enums;

namespace Municorn.Infrastructure.Models
{
    public class NotificationDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Id { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 1)]
        public string NotificationJson { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        [Required]
        public NotificationStatus Status { get; set; }

        public NotificationDto(string id, string notificationJson, NotificationType type = default, NotificationStatus status = default)
        {
            Id = id;
            NotificationJson = notificationJson;
            Type = type;
            Status = status;
        }
    }
}