using System;
using System.Collections.Generic;

namespace VivesRental.Model
{
    public class RentalOrder
    {
        public RentalOrder()
        {
            RentalOrderLines = new List<RentalOrderLine>();
        }
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

        public string UserFirstName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreatedAt { get; set; }

        public IList<RentalOrderLine> RentalOrderLines { get; set; }
    }
}
