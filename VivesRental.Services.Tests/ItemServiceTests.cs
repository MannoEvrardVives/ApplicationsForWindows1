using Microsoft.VisualStudio.TestTools.UnitTesting;
using VivesRental.Services.Factories;
using VivesRental.Tests.Data.Factories;

namespace VivesRental.Services.Tests
{
	[TestClass]
	public class ItemServiceTests
	{
		[TestMethod]
		public void Remove_Deletes_Item()
		{

			var unitOfWorkFactory = new UnitOfWorkFactory();
			//Arrange
			var itemService = new ItemService(unitOfWorkFactory);
			var rentalItemService = new RentalItemService(unitOfWorkFactory);

			var itemToAdd = ItemFactory.CreateValidEntity();
			var newItem = itemService.Create(itemToAdd);

			//Act
			var result = itemService.Remove(newItem.Id);

			//Assert
			Assert.IsTrue(result);

		}

		[TestMethod]
		public void Remove_Deletes_Item_With_RentalItems()
		{
			
				var unitOfWorkFactory = new UnitOfWorkFactory();
				//Arrange
				var itemService = new ItemService(unitOfWorkFactory);
				var rentalItemService = new RentalItemService(unitOfWorkFactory);

				var itemToAdd = ItemFactory.CreateValidEntity();
				var newItem = itemService.Create(itemToAdd);

				var rentalItem = RentalItemFactory.CreateValidEntity(newItem);
				rentalItemService.Create(rentalItem);

				//Act
				var result = itemService.Remove(newItem.Id);

				//Assert
				Assert.IsTrue(result);
			
		}

		[TestMethod]
		public void Remove_Deletes_Item_With_RentalItems_And_RentalOrderLines()
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
			var result = itemService.Remove(item.Id);

			//Assert
			Assert.IsTrue(result);

		}
	}
}
