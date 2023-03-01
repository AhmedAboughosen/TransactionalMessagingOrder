namespace TransactionalMessagingOrder.Persistence
{
    public class UnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        private InBoxRepository? _inBoxRepository;

        public InBoxRepository InBoxRepository
        {
            get
            {
                if (_inBoxRepository != null)
                    return _inBoxRepository;
                return _inBoxRepository = new InBoxRepository(_appDbContext);
            }
        }


        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }


        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}