using Municorn.DAL.Entities;
using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Models;

namespace Municorn.Infrastructure.Mappers
{
    public static class NotificationMapper
    {
        public static Notification CreateEntity(NotificationDto dto)
            => new()
            {
                Id = dto.Id,
                JsonObject = dto.NotificationJson,
                Status = (short)dto.Status,
                Type = (short)dto.Type
            };

        public static NotificationDto CreateDto(Notification entity)
            => new(entity.Id, entity.JsonObject, (NotificationType)entity.Type, (NotificationStatus)entity.Status);

        public static void FromDtoToEntity(NotificationDto dto, ref Notification entity)
        {
            entity.Id = dto.Id;
            entity.JsonObject = dto.NotificationJson;
            entity.Status = (short)dto.Status;
            entity.Type = (short)dto.Type;
        }
    }
}
