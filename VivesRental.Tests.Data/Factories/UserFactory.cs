using VivesRental.Model;

namespace VivesRental.Tests.Data.Factories
{
    public static class UserFactory
    {
        public static User CreateValidEntity()
        {
            return new User
            {
                FirstName = "TestFirstName",
                Name = "TestName",
                Email = "TestEmail",
                PhoneNumber = "TestPhoneNumber"
            };
        }

        public static User CreateInvalidEntity()
        {
            return new User
            {
                PhoneNumber = "TestPhoneNumber"
            };
        }
    }
}
