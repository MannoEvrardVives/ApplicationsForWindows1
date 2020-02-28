using System.Collections.Generic;

namespace VivesRental.Model
{
    public class RentalItem
    {
        public RentalItem(Item item)
        {
            ItemId = item.Id;
            Status = RentalItemStatus.Normal;
            RentalOrderLines = new List<RentalOrderLine>();
        }

        public RentalItem()
        {
            RentalOrderLines = new List<RentalOrderLine>();
        }

        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public RentalItemStatus Status { get; set; }

        public IList<RentalOrderLine> RentalOrderLines { get; set; }
    }

    public enum RentalItemStatus
    {
        Normal = 0,
        Broken = 1,
        InRepair = 2,
        Lost = 3,
        Destroyed = 4
    }
}
