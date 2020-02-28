using System;
using System.Diagnostics;
using VivesRental.Model;

namespace VivesRental.Services.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsValid(this Item item)
        {

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return false;
            }
               

            return true;
        }

        public static bool IsValid(this RentalItem rentalItem)
        {
            if (rentalItem.ItemId == 0)
                return false;
            
            return true;
        }

        public static bool IsValid(this RentalOrder rentalOrder)
        {
            if (rentalOrder.UserId == 0)
                return false;

            if (string.IsNullOrWhiteSpace(rentalOrder.UserFirstName))
                return false;

            if (string.IsNullOrWhiteSpace(rentalOrder.UserName))
                return false;

            if (string.IsNullOrWhiteSpace(rentalOrder.UserEmail))
                return false;

            if (rentalOrder.CreatedAt == DateTime.MinValue)
                return false;

            return true;
        }

        public static bool IsValid(this RentalOrderLine rentalOrderLine)
        {
            if (rentalOrderLine.RentalOrderId == 0)
                return false;

            if (rentalOrderLine.RentalItemId == 0)
                return false;

            if (string.IsNullOrWhiteSpace(rentalOrderLine.ItemName))
                return false;

            if (rentalOrderLine.RentedAt == DateTime.MinValue)
                return false;

            if (rentalOrderLine.ExpiresAt == DateTime.MinValue)
                return false;

            return true;
        }

        public static bool IsValid(this User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
                return false;

            if (string.IsNullOrWhiteSpace(user.Name))
                return false;

            if (string.IsNullOrWhiteSpace(user.Email))
                return false;

            return true;
        }
    }
}
