using System;
using System.Collections.Generic;
using VivesRental.Model;

namespace VivesRental.Services.Contracts
{
    public interface IRentalOrderService
    {
        RentalOrder Get(int id);
	    IList<RentalOrder> All();
	    RentalOrder Create(int userId);
		bool Return(int rentalOrderId, DateTime returnedAt);
    }
}
