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
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(DbContext context) : base(context)
        {
        }

	    public Item Get(int id, ItemIncludes includes)
	    {
		    var query = Context.Set<Item>().AsQueryable(); //It needs to be a queryable to be able to build the expression
		    query = AddIncludes(query, includes);
		    query = query.Where(i => i.Id == id); //Add the where clause
		    return query.FirstOrDefault(); 
	    }

		public IEnumerable<Item> All(ItemIncludes includes)
		{
			var query = Context.Set<Item>().AsQueryable(); //It needs to be a queryable to be able to build the expression
			query = AddIncludes(query, includes);
			return query.AsEnumerable(); 
		}

		public IEnumerable<Item> Find(Expression<Func<Item, bool>> predicate, ItemIncludes includes)
		{
			var query = Context.Set<Item>().AsQueryable(); //It needs to be a queryable to be able to build the expression
			query = AddIncludes(query, includes);
			return query.Where(predicate).AsEnumerable(); //Add the where clause and return IEnumerable<Item>
		}

		/// <summary>
		/// Adds the DbContext includes based on the booleans set in the Includes object
		/// </summary>
		/// <param name="query"></param>
		/// <param name="includes"></param>
		/// <returns></returns>
	    private IQueryable<Item> AddIncludes(IQueryable<Item> query, ItemIncludes includes)
	    {
		    if (includes == null)
			    return query;

		    if (includes.RentalItems)
			    query = query.Include(i => i.RentalItems);

		    if (includes.RentalItemsRentalOrderLines)
		    {
			    query = query.Include(i => i.RentalItems.Select(ri => ri.RentalOrderLines));
		    }

		    return query;
	    }

		public VivesRentalDbContext VivesRentalDbContext => Context as VivesRentalDbContext;
    }
}
