using TransactionalMessagingOrder.Events.DataTypes;

namespace TransactionalMessagingOrder.Entities
{
    public class InboxMessage
    {
        private InboxMessage()
        {
        }

        public InboxMessage(StudentCreatedData data)
        {
            StudentName = data.Name;
            StudentId = data.Id;
            StudentPhoneNumber = data.PhoneNumber;
        }

        public static InboxMessage FromId(long id) => new() { Id = id };

        public long Id { get; private set; }
        public Guid StudentId { get; private set; }
        public string StudentName { get; private set; }
        public string StudentPhoneNumber { get; private set; }

    }
}