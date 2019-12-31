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

        public RentalItem(Model.Item item)
        {
            ItemId = item.Id;
            Item = item;
            Status = RentalItemStatus.Normal;
        }

        public int Id { get; set; }
        public int ItemId { get; set; }
        public Model.Item Item { get; set; }
        public RentalItemStatus Status { get; set; }
    }
}
