using System;
using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Repository.Includes;
using VivesRental.Services.Contracts;
using VivesRental.Services.Exceptions;
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
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalItems.Get(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public RentalItem Get(int id, RentalItemIncludes includes)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalItems.Get(id, includes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IList<RentalItem> All()
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalItems.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IList<RentalItem> All(RentalItemIncludes includes)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalItems.All(includes).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IList<RentalItem> GetAvailableRentalItems()
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                             ri.RentalOrderLines.All(rol => rol.ReturnedAt.HasValue)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IList<RentalItem> GetAvailableRentalItems(RentalItemIncludes includes)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Normal &&
                                                             ri.RentalOrderLines.All(rol => rol.ReturnedAt.HasValue), includes).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IList<RentalItem> GetRentedRentalItems()
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Rented &&
                                                             ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IList<RentalItem> GetRentedRentalItems(RentalItemIncludes includes)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Rented &&
                                                             ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue), includes).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IList<RentalItem> GetRentedRentalItems(int userId)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Rented &&
                                                             ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue && rol.RentalOrder.UserId == userId)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IList<RentalItem> GetRentedRentalItems(int userId, RentalItemIncludes includes)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalItems.Find(ri => ri.Status == RentalItemStatus.Rented &&
                                                             ri.RentalOrderLines.Any(rol => !rol.ReturnedAt.HasValue && rol.RentalOrder.UserId == userId), includes).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public RentalItem Create(RentalItem entity)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    if (!entity.IsValid())
                    {
                        throw new InvalidInputException();
                    }

                    var rentalItem = new RentalItem
                    {
                        ItemId = entity.ItemId,
                        Status = entity.Status
                    };

                    unitOfWork.RentalItems.Add(rentalItem);
                    var numberOfObjectsUpdated = unitOfWork.Complete();
                    if (numberOfObjectsUpdated > 0)
                    {
                        return rentalItem;
                    }

                    throw new OperationFailedException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public RentalItem Edit(RentalItem entity)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {

                    if (!entity.IsValid())
                    {
                        throw new InvalidInputException();
                    }

                    //Get Item from unitOfWork
                    var rentalItem = unitOfWork.RentalItems.Get(entity.Id);
                    if (rentalItem == null)
                    {
                        throw new RentalItemNotFoundException();
                    }

                    //Only update the properties we want to update
                    rentalItem.ItemId = entity.ItemId;
                    rentalItem.Status = entity.Status;

                    var numberOfObjectsUpdated = unitOfWork.Complete();
                    if (numberOfObjectsUpdated > 0)
                    {
                        return entity;
                    }
                    throw new OperationFailedException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool Remove(int id)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    var rentalItem = unitOfWork.RentalItems.Get(id, new RentalItemIncludes { RentalOrderLines = true });
                    if (rentalItem == null)
                        throw new RentalItemNotFoundException();

                    foreach (var rentalOrderLine in rentalItem.RentalOrderLines)
                    {
                        rentalOrderLine.RentalItem = null;
                        rentalOrderLine.RentalItemId = null;
                    }

                    unitOfWork.RentalItems.Remove(rentalItem);

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
    }
}
