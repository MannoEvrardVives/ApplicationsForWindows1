using System;
using System.Diagnostics;
using VivesRental.Repository.Contracts;
using VivesRental.Repository.Exceptions;

namespace VivesRental.Repository.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VivesRentalDbContext _context;

        public UnitOfWork()
        {
            string connectionString = "server=MANNOBC96\\SQLEXPRESS;database=VivesRental;trusted_connection=true;";
            _context = new VivesRentalDbContext(connectionString);

            if (!_context.Database.Exists())
            {
                throw new NoDatabaseConnectionException();
            }

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
