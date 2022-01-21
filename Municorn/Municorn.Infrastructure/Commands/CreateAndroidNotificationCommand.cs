using Municorn.Infrastructure.Requests;

namespace Municorn.Infrastructure.Commands
{
    public record CreateAndroidNotificationCommand : CreateNotificationBaseCommand<CreateAndroidNotificationRequest>
    {
        public CreateAndroidNotificationCommand(CreateAndroidNotificationRequest data) : base(data) { }
    }
}
