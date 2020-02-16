using System;
using System.Collections.Generic;
namespace VivesRental.GUI.Models
{
    class RentalOrder
    {

        public RentalOrder(int userId, IList<Item> rentedItems)
        {
            UserId = userId;
            CreatedAt = DateTime.Now;
            RentalOrderLines = new List<RentalOrderLine>();
            foreach (var item in rentedItems)
            {
                foreach (var rentalItem in item.RentalItems)
                {
                    
                    RentalOrderLines.Add(new RentalOrderLine(rentalItem, item.Name, item.Description));
                }
            }
        }

        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public IList<RentalOrderLine> RentalOrderLines { get; set; }
    }
}
