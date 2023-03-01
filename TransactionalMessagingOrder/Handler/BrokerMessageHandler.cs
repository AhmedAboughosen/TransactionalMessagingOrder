using TransactionalMessagingOrder.Entities;
using TransactionalMessagingOrder.Events;
using TransactionalMessagingOrder.Persistence;

namespace TransactionalMessagingOrder.Handler
{
    public class BrokerMessageHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BrokerMessageHandler> _logger;
        private int _lockedScopes;

        private static readonly object LockObject = new();

        public BrokerMessageHandler(IServiceProvider serviceProvider, IConfiguration configuration,
            ILogger<BrokerMessageHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void StartHandling()
        {
            // Don't wait .
            Task.Run(PublishNonPublishedNotification);
        }

        private void PublishNonPublishedNotification()
        {

            if (_lockedScopes > 2)
                return;

            lock (LockObject)
            {
                _lockedScopes++;

                try
                {

                    using var scope = _serviceProvider.CreateScope();

                    var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();

                    var messages = unitOfWork.InBoxRepository.GetAllAsync().GetAwaiter().GetResult();

                    _logger.LogInformation("Fetched Message From outbox {Count}", messages);

                    PublishAndRemoveMessagesAsync(messages, unitOfWork).GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e, "Notification Publisher error.");
                }
                finally
                {
                    _lockedScopes--;
                }
            }
        }

        private async Task PublishAndRemoveMessagesAsync(IEnumerable<InboxMessage> messages, UnitOfWork unitOfWork)
        {
            foreach (var message in messages)
            {
                await HandlerMessageAsync(message);

                await unitOfWork.InBoxRepository.RemoveAsync(InboxMessage.FromId(message.Id));


                await unitOfWork.SaveChangesAsync();
            }

            await Task.CompletedTask;
        }


        private async Task HandlerMessageAsync(InboxMessage message)
        {
          
            
            //business Logic for add student
        }
    }
}