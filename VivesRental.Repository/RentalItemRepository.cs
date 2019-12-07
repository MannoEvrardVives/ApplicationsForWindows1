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
    public class RentalItemRepository : Repository<RentalItem>, IRentalItemRepository
    {
        public RentalItemRepository(DbContext context) : base(context)
        {
        }

        public RentalItem Get(int id, RentalItemIncludes includes)
        {
            var query = Context.Set<RentalItem>().AsQueryable(); //It needs to be a queryable to be able to build the expression
            query = AddIncludes(query, includes);
            query = query.Where(i => i.Id == id); //Add the where clause
            return query.FirstOrDefault();
        }

        public IEnumerable<RentalItem> Find(Expression<Func<RentalItem, bool>> predicate, RentalItemIncludes includes)
        {
            var query = Context.Set<RentalItem>().AsQueryable(); //It needs to be a queryable to be able to build the expression
            query = AddIncludes(query, includes);
            return query.Where(predicate).AsEnumerable(); //Add the where clause and return IEnumerable<RentalItem>
        }

        public IEnumerable<RentalItem> All(RentalItemIncludes includes)
        {
            var query = Context.Set<RentalItem>().AsQueryable(); //It needs to be a queryable to be able to build the expression
            query = AddIncludes(query, includes);
            return query.AsEnumerable();
        }

        private IQueryable<RentalItem> AddIncludes(IQueryable<RentalItem> query, RentalItemIncludes includes)
        {
            if (includes == null)
                return query;

            if (includes.Item)
                query = query.Include(i => i.Item);

	        if (includes.RentalOrderLines)
		        query = query.Include(i => i.RentalOrderLines);

            return query;
        }

        public VivesRentalDbContext VivesRentalDbContext => Context as VivesRentalDbContext;
    }
}
