using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Model;

namespace VivesRental.GUI.Models
{
    class RentalItem : Model.RentalItem
    {

        public RentalItem(Models.Item item)
        {
            Item = item;
            ItemId = item.Id;
            Status = RentalItemStatus.Normal;
            RentalOrderLines = new List<Models.RentalOrderLine>();
        }

        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public RentalItemStatus Status { get; set; }

        public IList<Models.RentalOrderLine> RentalOrderLines { get; set; }
    }
}
