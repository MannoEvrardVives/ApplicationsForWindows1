using Microsoft.VisualStudio.TestTools.UnitTesting;
using VivesRental.Services.Extensions;
using VivesRental.Tests.Data.Factories;

namespace VivesRental.Services.Tests
{
    [TestClass]
    public class ValidationExtensionsTests
    {
        [TestMethod]
        public void Item_IsValid_Returns_True_When_Valid()
        {
            //Arrange
            var item = ItemFactory.CreateValidEntity();

            //Act
            var result = item.IsValid();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Item_IsValid_Returns_False_When_Invalid()
        {
            //Arrange
            var item = ItemFactory.CreateInvalidEntity();

            //Act
            var result = item.IsValid();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void User_IsValid_Returns_True_When_Valid()
        {
            //Arrange
            var user = UserFactory.CreateValidEntity();

            //Act
            var result = user.IsValid();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_IsValid_Returns_False_When_Invalid()
        {
            //Arrange
            var user = UserFactory.CreateInvalidEntity();

            //Act
            var result = user.IsValid();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RentalItem_IsValid_Returns_True_When_Valid()
        {
            //Arrange
            var item = ItemFactory.CreateValidEntity();
            item.Id = 1;
            var rentalItem = RentalItemFactory.CreateValidEntity(item);
            rentalItem.Id = 1;

            //Act
            var result = rentalItem.IsValid();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RentalItem_IsValid_Returns_False_When_Invalid()
        {
            //Arrange
            var rentalItem = RentalItemFactory.CreateInvalidEntity();

            //Act
            var result = rentalItem.IsValid();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RentalOrder_IsValid_Returns_True_When_Valid()
        {
            //Arrange
            var user = UserFactory.CreateValidEntity();
            user.Id = 1;
            var rentalOrder = RentalOrderFactory.CreateValidEntity(user);
            rentalOrder.Id = 1;

            //Act
            var result = rentalOrder.IsValid();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RentalOrder_IsValid_Returns_False_When_Invalid()
        {
            //Arrange
            var rentalOrder = RentalOrderFactory.CreateInvalidEntity();

            //Act
            var result = rentalOrder.IsValid();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RentalOrderLine_IsValid_Returns_True_When_Valid()
        {
            //Arrange
            var user = UserFactory.CreateValidEntity();
            user.Id = 1;
            var rentalOrder = RentalOrderFactory.CreateValidEntity(user);
            rentalOrder.Id = 1;
            var item = ItemFactory.CreateValidEntity();
            item.Id = 1;
            var rentalItem = RentalItemFactory.CreateValidEntity(item);
            rentalItem.Id = 1;
            var rentalOrderLine = RentalOrderLineFactory.CreateValidEntity(rentalOrder, rentalItem);
            rentalOrderLine.Id = 1;

            //Act
            var result = rentalOrderLine.IsValid();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RentalOrderLine_IsValid_Returns_False_When_Invalid()
        {
            //Arrange
            var rentalOrder = RentalOrderFactory.CreateInvalidEntity();

            //Act
            var result = rentalOrder.IsValid();

            //Assert
            Assert.IsFalse(result);
        }
    }
}
