using System;
using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Services.Contracts;
using VivesRental.Services.Exceptions;
using VivesRental.Services.Factories;

namespace VivesRental.Services
{

    public class RentalOrderService: IRentalOrderService
    {
	    private readonly IUnitOfWorkFactory unitOfWorkFactory;

	    public RentalOrderService()
	    {
		    this.unitOfWorkFactory = new UnitOfWorkFactory();
	    }

	    public RentalOrderService(IUnitOfWorkFactory unitOfWorkFactory)
	    {
		    this.unitOfWorkFactory = unitOfWorkFactory;
	    }

		public RentalOrder Get(int id)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    var rentalOrder = unitOfWork.RentalOrders.Get(id);
                    if (rentalOrder == null)
                    {
                        throw new RentalOrderNotFoundException();
                    }

                    return rentalOrder;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IList<RentalOrder> All()
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.RentalOrders.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public RentalOrder Create(int userId)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    var user = unitOfWork.Users.Get(userId);
                    if (user == null)
                    {
                        throw new UserNotFoundException();
                    }

                    var rentalOrder = new RentalOrder
                    {
                        UserId = user.Id,
                        UserFirstName = user.FirstName,
                        UserName = user.Name,
                        UserEmail = user.Email,
                        CreatedAt = DateTime.Now
                    };

                    unitOfWork.RentalOrders.Add(rentalOrder);
                    var numberOfObjectsUpdated = unitOfWork.Complete();
                    if (numberOfObjectsUpdated > 0)
                    {
                        return rentalOrder;
                    }
                    throw new OperationFailedException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool Return(int rentalOrderId, DateTime returnedAt)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    var rentalOrderLines = unitOfWork.RentalOrderLines.Find(rol => rol.RentalOrderId == rentalOrderId);
                    foreach (var rentalOrderLine in rentalOrderLines)
                    {
                        rentalOrderLine.ReturnedAt = returnedAt;
                    }

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
