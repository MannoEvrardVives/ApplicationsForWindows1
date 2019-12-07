using System;
using System.Threading.Tasks;
using VivesRental.Repository.Contracts;

namespace VivesRental.Repository.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository Items { get; }
        IRentalItemRepository RentalItems { get; }
        IRentalOrderRepository RentalOrders { get; }
        IRentalOrderLineRepository RentalOrderLines { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
