using Municorn.Infrastructure.Requests;

namespace Municorn.Infrastructure.Commands
{
    public record CreateiOSNotificationCommand : CreateNotificationBaseCommand<CreateiOSNotificationRequest>
    {
        public CreateiOSNotificationCommand(CreateiOSNotificationRequest data) : base(data) { }
    }
}
