using MediatR;
using TransactionalMessagingOrder.Events;
using TransactionalMessagingOrder.Persistence;

namespace TransactionalMessagingOrder.BaseService
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IServiceProvider _provider;
        private UnitOfWork _unitOfWork;

        public MessageHandler(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<bool> HandleAsync<T>(MessageBody<T> message)
        {
            using var scope = _provider.CreateScope();

            var medaiator = scope.ServiceProvider.GetRequiredService<IMediator>();

            return await medaiator.Send(message);
        }
    }
}