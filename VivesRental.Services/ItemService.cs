using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VivesRental.Model;
using VivesRental.Repository.Includes;
using VivesRental.Services.Contracts;
using VivesRental.Services.Exceptions;
using VivesRental.Services.Extensions;
using VivesRental.Services.Factories;

namespace VivesRental.Services
{
    public class ItemService: IItemService
    {
	    private readonly IUnitOfWorkFactory unitOfWorkFactory;
	    public ItemService()
	    {
		    this.unitOfWorkFactory = new UnitOfWorkFactory();
	    }

	    public ItemService(IUnitOfWorkFactory unitOfWorkFactory)
	    {
		    this.unitOfWorkFactory = unitOfWorkFactory;
	    }

        public Item Get(int id)
        {
            try
            {
                return Get(id, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
	       
        }

	    public Item Get(int id, ItemIncludes includes)
	    {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.Items.Get(id, includes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
		    
	    }

		public IList<Item> All()
        {
            try
            {
                return All(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

	    public IList<Item> All(ItemIncludes includes)
	    {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.Items.All(includes).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
	    }

		public Item Create(Item entity)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    if (!entity.IsValid())
                    {
                        throw new InvalidInputException();
                    }

                    //Add a clean item
                    var item = new Item
                    {
                        Name = entity.Name,
                        Description = entity.Description,
                        Manufacturer = entity.Manufacturer,
                        Publisher = entity.Publisher,
                        RentalExpiresAfterDays = entity.RentalExpiresAfterDays
                    };

                    unitOfWork.Items.Add(item);
                    var numberOfObjectsUpdated = unitOfWork.Complete();
                    if (numberOfObjectsUpdated > 0)
                    {
                        return item;
                    }
                    throw new OperationFailedException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public Item Edit(Item entity)
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
                    var item = unitOfWork.Items.Get(entity.Id);
                    if (item == null)
                    {
                        throw new ItemNotFoundException();
                    }

                    //Only update the properties we want to update
                    item.Name = entity.Name;
                    item.Description = entity.Description;
                    item.Manufacturer = entity.Manufacturer;
                    item.Publisher = entity.Publisher;
                    item.RentalExpiresAfterDays = entity.RentalExpiresAfterDays;

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
                    var item = unitOfWork.Items.Get(id, new ItemIncludes { RentalItemsRentalOrderLines = true });
                    if (item == null)
                        throw new ItemNotFoundException();

                    //Remove rentalItems
                    foreach (var rentalItem in item.RentalItems.ToList())
                    {
                        //Remove rentalOrderLines
                        foreach (var rentalOrderLine in rentalItem.RentalOrderLines.ToList())
                        {
                            rentalOrderLine.RentalItem = null;
                            rentalOrderLine.RentalItemId = null;
                        }
                        unitOfWork.RentalItems.Remove(rentalItem);
                    }
                    //Remove item
                    unitOfWork.Items.Remove(item);

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
