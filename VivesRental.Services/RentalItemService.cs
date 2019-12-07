using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Repository.Includes;
using VivesRental.Services.Contracts;
using VivesRental.Services.Extensions;
using VivesRental.Services.Factories;

namespace VivesRental.Services
{
    public class RentalItemService : IRentalItemService
    {
	    private readonly IUnitOfWorkFactory unitOfWorkFactory;

	    public RentalItemService()
	    {
		    this.unitOfWorkFactory = new UnitOfWorkFactory();
	    }

	    public RentalItemService(IUnitOfWorkFactory unitOfWorkFactory)
	    {
		    this.unitOfWorkFactory = unitOfWorkFactory;
	    }
		public RentalItem Get(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalItems.Get(id);
            }
        }

        public RentalItem Get(int id, RentalItemIncludes includes)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalItems.Get(id, includes);
            }
        }

        public IList<RentalItem> All()
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalItems.GetAll().ToList();
            }
        }

        public IList<RentalItem> All(RentalItemIncludes includes)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalItems.All(includes).ToList();
            }
        }

        public IList<RentalItem> GetAvailableRentalItems()
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.All(rol => rol.ReturnedAt.HasValue)).ToList();
            }
        }

        public IList<RentalItem> GetAvailableRentalItems(RentalItemIncludes includes)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.All(rol => rol.ReturnedAt.HasValue), includes).ToList();
            }
        }

        public IList<RentalItem> GetRentedRentalItems()
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue)).ToList();
            }
        }

        public IList<RentalItem> GetRentedRentalItems(RentalItemIncludes includes)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue), includes).ToList();
            }
        }

        public IList<RentalItem> GetRentedRentalItems(int userId)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue && rol.RentalOrder.UserId == userId)).ToList();
            }
        }

        public IList<RentalItem> GetRentedRentalItems(int userId, RentalItemIncludes includes)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                         ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue && rol.RentalOrder.UserId == userId), includes).ToList();
            }
        }

        public RentalItem Create(RentalItem entity)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                if (!entity.IsValid())
                {
                    return null;
                }

				var rentalItem = new RentalItem
				{
					ItemId = entity.ItemId,
					Status = entity.Status
				};

                unitOfWork.RentalItems.Add(rentalItem);
                var numberOfObjectsUpdated = unitOfWork.Complete();
                if( numberOfObjectsUpdated > 0)
                {
                    return rentalItem;
                }
                return null;
            }
        }

        public RentalItem Edit(RentalItem entity)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {

                if (!entity.IsValid())
                {
                    return null;
                }

                //Get Item from unitOfWork
                var rentalItem = unitOfWork.RentalItems.Get(entity.Id);
                if (rentalItem == null)
                {
                    return null;
                }

                //Only update the properties we want to update
                rentalItem.ItemId = entity.ItemId;
                rentalItem.Status = entity.Status;

                var numberOfObjectsUpdated = unitOfWork.Complete();
                if( numberOfObjectsUpdated > 0)
                {
                    return entity;
                }
                return null;
            }
        }

        public bool Remove(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                var rentalItem = unitOfWork.RentalItems.Get(id, new RentalItemIncludes{RentalOrderLines = true});
                if (rentalItem == null)
                    return false;

	            foreach (var rentalOrderLine in rentalItem.RentalOrderLines)
	            {
		            rentalOrderLine.RentalItem = null;
		            rentalOrderLine.RentalItemId = null;
	            }

                unitOfWork.RentalItems.Remove(rentalItem);

                var numberOfObjectsUpdated = unitOfWork.Complete();
                return numberOfObjectsUpdated > 0;
            }
        }
    }
}
