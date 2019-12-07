using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VivesRental.Model;
using VivesRental.Repository.Core;
using VivesRental.Repository.Includes;

namespace VivesRental.Repository.Contracts
{
    public interface IItemRepository: IRepository<Item>
    {
	    IEnumerable<Item> All(ItemIncludes includes);

		IEnumerable<Item> Find(Expression<Func<Item, bool>> predicate, ItemIncludes includes);
		Item Get(int id, ItemIncludes includes);
	}
}
