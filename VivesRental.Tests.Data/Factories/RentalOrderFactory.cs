using System;
using VivesRental.Model;

namespace VivesRental.Tests.Data.Factories
{
    public static class RentalOrderFactory
    {
        public static RentalOrder CreateValidEntity(User user)
        {
            return new RentalOrder
            {
                UserId = user.Id,
                User = user,
                UserFirstName = "TestUserFirstName",
                UserName = "TestUserName",
                UserEmail = "TestUserEmail",
                CreatedAt = DateTime.Now
            };
        }

        public static RentalOrder CreateInvalidEntity()
        {
            return new RentalOrder
            {
                CreatedAt = DateTime.Now
            };
        }
    }
}
