using System.Collections.Generic;
using VivesRental.Model;

namespace VivesRental.Services.Contracts
{
    public interface IRentalItemService
    {
        RentalItem Get(int id);
	    IList<RentalItem> All();
	    IList<RentalItem> GetAvailableRentalItems();
	    IList<RentalItem> GetRentedRentalItems();
	    IList<RentalItem> GetRentedRentalItems(int userId);

		RentalItem Create(RentalItem entity);
        RentalItem Edit(RentalItem entity);
        bool Remove(int id);
    }
}
