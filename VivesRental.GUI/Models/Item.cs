using System.Collections.Generic;
using VivesRental.Model;

namespace VivesRental.GUI.Models
{
    public class Item : Model.Item
    {
        public Item(Model.Item item)
        {
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            Manufacturer = item.Manufacturer;
            Publisher = item.Publisher;
            RentalExpiresAfterDays = item.RentalExpiresAfterDays;
            RentalItems = new List<RentalItem>();
        }

        public Item()
        {
            RentalItems = new List<RentalItem>();
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Publisher { get; set; }
        public int RentalExpiresAfterDays { get; set; }

        private IList<RentalItem> RentalItems { get; set; }
    }
}