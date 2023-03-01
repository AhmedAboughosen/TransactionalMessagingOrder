using TransactionalMessagingOrder.Events.DataTypes;

namespace TransactionalMessagingOrder.Entities
{
    public class Student
    {
        private Student()
        {
        }

        public Student(StudentCreatedData data)
        {
            Name = data.Name;
            Id = data.Id;
            PhoneNumber = data.PhoneNumber;
        }


        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }

    }
}