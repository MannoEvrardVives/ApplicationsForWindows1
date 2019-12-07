using System;
using System.Collections.Generic;
using VivesRental.Model;

namespace VivesRental.Services.Contracts
{
    public interface IRentalOrderLineService
    {
        RentalOrderLine Get(int id);
        bool Rent(int rentalOrderId, int rentalItemId);
        bool Return(int rentalOrderLineId, DateTime returnedAt);
	    IList<RentalOrderLine> FindByRentalOrderId(int rentalOrderId);

    }
}
