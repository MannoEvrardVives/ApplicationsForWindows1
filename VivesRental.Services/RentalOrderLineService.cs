using System;
using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Services.Contracts;
using VivesRental.Services.Exceptions;
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
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalOrderLines.Get(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IList<RentalOrderLine> FindByRentalOrderId(int rentalOrderId)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalOrderLines.Find(rol => rol.RentalOrderId == rentalOrderId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool Rent(int rentalOrderId, int rentalItemId)
        {
            try
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
                    if (numberOfObjectsUpdated > 0)
                        return true;
                    throw new OperationFailedException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    var rentalOrderLine = unitOfWork.RentalOrderLines.Get(rentalOrderLineId);

                    if (rentalOrderLine == null)
                    {
                        throw new RentalOrderLineNotFoundException();
                    }

                    if (returnedAt == DateTime.MinValue)
                    {
                        throw new InvalidInputException();
                    }

                    if (rentalOrderLine.ReturnedAt.HasValue)
                    {
                        throw new InvalidInputException();
                    }

                    rentalOrderLine.ReturnedAt = returnedAt;

                    unitOfWork.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
