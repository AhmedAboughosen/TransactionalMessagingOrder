using Core.Domain.Events;
using TransactionalMessagingOrder.Events;

namespace TransactionalMessagingOrder.BaseService
{
    public interface IMessageHandler
    {
        Task<bool> HandleAsync<T>(MessageBody<T> message);
    }
    
}