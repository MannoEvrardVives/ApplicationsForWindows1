using Microsoft.VisualStudio.TestTools.UnitTesting;
using VivesRental.Repository.Core;
using VivesRental.Services.Factories;
using VivesRental.Tests.Data.Factories;
using VivesRental.Tests.Data.Helpers;

namespace VivesRental.Services.Tests
{
	[TestClass]
	public class RentalItemServiceTests
	{
		[TestInitialize]
		public void TestInitialize()
		{
			//Clear database
			DbContextHelper.Clear(new VivesRentalDbContext());
		}

		[TestMethod]
		public void Remove_Deletes_RentalItem()
		{

			var unitOfWorkFactory = new UnitOfWorkFactory();
			//Arrange
			var itemService = new ItemService(unitOfWorkFactory);
			var rentalItemService = new RentalItemService(unitOfWorkFactory);

			var itemToAdd = ItemFactory.CreateValidEntity();
			var newItem = itemService.Create(itemToAdd);

			var rentalItem = RentalItemFactory.CreateValidEntity(newItem);
			var newRentalItem = rentalItemService.Create(rentalItem);

			//Act
			var result = rentalItemService.Remove(newRentalItem.Id);

			//Assert
			Assert.IsTrue(result);

		}

		[TestMethod]
		public void Remove_Deletes_RentalItem_With_RentalOrderLines()
		{

			var unitOfWorkFactory = new UnitOfWorkFactory();
			//Arrange
			var userService = new UserService(unitOfWorkFactory);
			var itemService = new ItemService(unitOfWorkFactory);
			var rentalItemService = new RentalItemService(unitOfWorkFactory);
			var rentalOrderService = new RentalOrderService(unitOfWorkFactory);
			var rentalOrderLineService = new RentalOrderLineService(unitOfWorkFactory);

			var userToAdd = UserFactory.CreateValidEntity();
			var user = userService.Create(userToAdd);
			var itemToAdd = ItemFactory.CreateValidEntity();
			var item = itemService.Create(itemToAdd);

			var rentalItemToAdd = RentalItemFactory.CreateValidEntity(item);
			var rentalItem = rentalItemService.Create(rentalItemToAdd);

			var rentalOrder = rentalOrderService.Create(user.Id);

			//var rentalOrderLineToAdd = RentalOrderLineFactory.CreateValidEntity(rentalOrder, rentalItem);
			var rentalOrderLine = rentalOrderLineService.Rent(rentalOrder.Id, rentalItem.Id);

			//Act
			var result = rentalItemService.Remove(rentalItem.Id);

			//Assert
			Assert.IsTrue(result);

		}
	}
}
