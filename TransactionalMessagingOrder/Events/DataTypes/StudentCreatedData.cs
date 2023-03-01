using MediatR;

namespace TransactionalMessagingOrder.Events.DataTypes
{
    public record StudentCreatedData(string Name, string PhoneNumber, Guid Id) : IRequest<bool>
    {
        public StudentCreatedData() : this(default, default, default)
        {
        }
    }
}