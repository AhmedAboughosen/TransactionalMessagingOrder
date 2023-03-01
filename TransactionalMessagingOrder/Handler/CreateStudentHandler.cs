using MediatR;
using TransactionalMessagingOrder.Entities;
using TransactionalMessagingOrder.Events.DataTypes;
using TransactionalMessagingOrder.Persistence;

namespace TransactionalMessagingOrder.Handler
{
    public class CreateStudentHandler : IRequestHandler<StudentCreatedData, bool>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly BrokerMessageHandler _brokerMessageHandler;

        public CreateStudentHandler(UnitOfWork unitOfWork, BrokerMessageHandler brokerMessageHandler)
        {
            _unitOfWork = unitOfWork;
            _brokerMessageHandler = brokerMessageHandler;
        }


        public async Task<bool> Handle(StudentCreatedData dto,
            CancellationToken cancellationToken)
        {
            var inboxMessage = new InboxMessage(dto);

            await _unitOfWork.InBoxRepository.AddAsync(inboxMessage);

            await _unitOfWork.SaveChangesAsync();

            _brokerMessageHandler.StartHandling();
            return true;
        }
    }
}