using System.Collections.Generic;

namespace VivesRental.Model
{
    public class User
    {
        public User()
        {
            RentalOrders = new List<RentalOrder>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public IList<RentalOrder> RentalOrders { get; set; }
    }
}
