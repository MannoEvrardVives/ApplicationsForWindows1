using System;
using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Services.Contracts;
using VivesRental.Services.Factories;

namespace VivesRental.Services
{
    public class RentalOrderLineService: IRentalOrderLineService
    {
	    private readonly IUnitOfWorkFactory unitOfWorkFactory;

	    public RentalOrderLineService()
	    {
		    this.unitOfWorkFactory = new UnitOfWorkFactory();
	    }

	    public RentalOrderLineService(IUnitOfWorkFactory unitOfWorkFactory)
	    {
		    this.unitOfWorkFactory = unitOfWorkFactory;
	    }

		public RentalOrderLine Get(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalOrderLines.Get(id);
            }
        }

        public IList<RentalOrderLine> FindByRentalOrderId(int rentalOrderId)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalOrderLines.Find(rol => rol.RentalOrderId == rentalOrderId).ToList();
            }
        }

        public bool Rent(int rentalOrderId, int rentalItemId)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                var rentalItem = unitOfWork.RentalItems.Get(rentalItemId);
                var item = unitOfWork.Items.Get(rentalItem.ItemId);
                var rentalOrderLine = new RentalOrderLine
                {
                    RentalItemId = rentalItemId,
                    RentalOrderId = rentalOrderId,
                    ItemName = item.Name,
                    ItemDescription = item.Description,
                    ExpiresAt = DateTime.Now.AddDays(item.RentalExpiresAfterDays),
                    RentedAt = DateTime.Now
                };

                unitOfWork.RentalOrderLines.Add(rentalOrderLine);
                var numberOfObjectsUpdated = unitOfWork.Complete();
                return numberOfObjectsUpdated > 0;
            }
        }

        /// <summary>
        /// Returns a rented item
        /// </summary>
        /// <param name="rentalOrderLineId"></param>
        /// <param name="returnedAt"></param>
        /// <returns></returns>
        public bool Return(int rentalOrderLineId, DateTime returnedAt)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                var rentalOrderLine = unitOfWork.RentalOrderLines.Get(rentalOrderLineId);

                if (rentalOrderLine == null)
                {
                    return false;
                }

                if (returnedAt == DateTime.MinValue)
                {
                    return false;
                }

                if (rentalOrderLine.ReturnedAt.HasValue)
                {
                    return false;
                }

                rentalOrderLine.ReturnedAt = returnedAt;

                unitOfWork.Complete();
                return true;
            }
        }
    }
}
