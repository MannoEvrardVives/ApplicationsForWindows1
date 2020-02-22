using System.Collections.Generic;

namespace VivesRental.Model
{
    public class Item
    {
        public Item(int id = 0)
        {

            // temp 

            Id = id;
            Name = "name van item";
            Description = "description van item";



            RentalItems = new List<RentalItem>();

            //temp
            for (var i = 1; i <= 2; i++)
            {
                RentalItems.Add(new RentalItem());
            }
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Publisher { get; set; }
        public int RentalExpiresAfterDays { get; set; }

        public IList<RentalItem> RentalItems { get; set; }
    }
}
