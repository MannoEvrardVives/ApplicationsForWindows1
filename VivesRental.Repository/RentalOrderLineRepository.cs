using System;
using System.Data.Entity;
using System.Linq;
using VivesRental.Model;
using VivesRental.Repository.Contracts;
using VivesRental.Repository.Core;

namespace VivesRental.Repository
{
    public class RentalOrderLineRepository : Repository<RentalOrderLine>, IRentalOrderLineRepository
    {
        public RentalOrderLineRepository(DbContext context) : base(context)
        {
        }
        
        public VivesRentalDbContext VivesRentalDbContext => Context as VivesRentalDbContext;
    }
}
