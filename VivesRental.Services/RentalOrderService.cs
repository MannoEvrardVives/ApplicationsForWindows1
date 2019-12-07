using System;
using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Services.Contracts;
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
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalOrders.Get(id);
            }
        }

        public IList<RentalOrder> All()
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.RentalOrders.GetAll().ToList();
            }
        }

        public RentalOrder Create(int userId)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
	            var user = unitOfWork.Users.Get(userId);
	            if (user == null)
	            {
		            return null;
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
                if( numberOfObjectsUpdated > 0)
                {
                    return rentalOrder;
                }
                return null;
            }
        }

        public bool Return(int rentalOrderId, DateTime returnedAt)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                var rentalOrderLines = unitOfWork.RentalOrderLines.Find(rol => rol.RentalOrderId == rentalOrderId);
                foreach (var rentalOrderLine in rentalOrderLines)
                {
                    rentalOrderLine.ReturnedAt = returnedAt;
                }

                var numberOfObjectsUpdated = unitOfWork.Complete();
                return numberOfObjectsUpdated > 0;
            }
        }
    }
}
