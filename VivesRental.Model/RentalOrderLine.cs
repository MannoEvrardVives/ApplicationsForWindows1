using System;

namespace VivesRental.Model
{
    public class RentalOrderLine
    {
        public int Id { get; set; }
        public int RentalOrderId { get; set; }
        public RentalOrder RentalOrder { get; set; }
        public int? RentalItemId { get; set; }
        public RentalItem RentalItem { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public DateTime RentedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
    }
}
