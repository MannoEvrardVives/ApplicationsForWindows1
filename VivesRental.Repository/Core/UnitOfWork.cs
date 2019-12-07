using VivesRental.Repository.Contracts;

namespace VivesRental.Repository.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VivesRentalDbContext _context;

        public UnitOfWork()
        {
            _context = new VivesRentalDbContext();
            Items = new ItemRepository(_context);
            RentalItems = new RentalItemRepository(_context);
            RentalOrders = new RentalOrderRepository(_context);
            RentalOrderLines = new RentalOrderLineRepository(_context);
            Users = new UserRepository(_context);
        }

        public IItemRepository Items { get; }
        public IRentalItemRepository RentalItems { get; }
        public IRentalOrderRepository RentalOrders { get; }
        public IRentalOrderLineRepository RentalOrderLines { get; }
        public IUserRepository Users { get; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
