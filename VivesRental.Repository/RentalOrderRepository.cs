using System.Data.Entity;
using VivesRental.Model;
using VivesRental.Repository.Contracts;
using VivesRental.Repository.Core;

namespace VivesRental.Repository
{
    public class RentalOrderRepository : Repository<RentalOrder>, IRentalOrderRepository
    {
        public RentalOrderRepository(DbContext context) : base(context)
        {
        }

        public VivesRentalDbContext VivesRentalDbContext => Context as VivesRentalDbContext;
    }
}
