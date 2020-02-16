using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Model;

namespace VivesRental.GUI.Models
{
    class RentalOrderLine
    {

        public RentalOrderLine(RentalItem rentalItem, string itemName, string itemDescription)
        {
            RentalItem = rentalItem;
            ItemName = itemName;
            ItemDescription = itemDescription;
            RentedAt = DateTime.Now;
        }

        public RentalItem RentalItem { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public DateTime RentedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
