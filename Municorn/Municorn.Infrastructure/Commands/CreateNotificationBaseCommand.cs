using MediatR;

using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Responses;

namespace Municorn.Infrastructure.Commands
{
    public abstract record CreateNotificationBaseCommand<T> : IRequest<CreateNotificationResponse> where T : class, ICreateNotificationRequest
    {
        public T Data { get; }

        public CreateNotificationBaseCommand(T data)
        {
            Data = data;
        }
    }
}
