using TransactionalMessagingOrder.Entities;

namespace TransactionalMessagingOrder.Persistence
{
    public class InBoxRepository : AsyncRepository<InboxMessage>
    {
        private readonly AppDbContext _appDbContext;

        public InBoxRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public override Task RemoveAsync(InboxMessage entity)
        {
            _appDbContext.Attach(entity);

            return base.RemoveAsync(entity);
        }
    }
}