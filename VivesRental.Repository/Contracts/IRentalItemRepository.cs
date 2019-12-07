using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VivesRental.Model;
using VivesRental.Repository.Core;
using VivesRental.Repository.Includes;

namespace VivesRental.Repository.Contracts
{
    public interface IRentalItemRepository: IRepository<RentalItem>
    {
        IEnumerable<RentalItem> All(RentalItemIncludes includes);

        IEnumerable<RentalItem> Find(Expression<Func<RentalItem, bool>> predicate, RentalItemIncludes includes);
        RentalItem Get(int id, RentalItemIncludes includes);
    }
}
