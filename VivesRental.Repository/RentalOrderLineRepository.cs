using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using VivesRental.Model;
using VivesRental.Repository.Contracts;
using VivesRental.Repository.Core;
using VivesRental.Repository.Includes;

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
