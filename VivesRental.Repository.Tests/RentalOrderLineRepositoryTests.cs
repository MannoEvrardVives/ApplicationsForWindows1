using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VivesRental.Repository.Core;
using VivesRental.Tests.Data.Factories;
using VivesRental.Tests.Data.Helpers;

namespace VivesRental.Repository.Tests
{
    [TestClass]
    public class RentalOrderLineRepositoryTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            //Clear database
            DbContextHelper.Clear(new VivesRentalDbContext());
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void Add_Throws_Exception_When_Adding_InValid_RentalOrder()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var rentalOrderLineRepository = new RentalOrderLineRepository(context);

                //Act
                var rentalOrderLine = RentalOrderLineFactory.CreateInvalidEntity();
                rentalOrderLineRepository.Add(rentalOrderLine);

                var result = context.SaveChanges();

                //No Assert
            }
            
        }

        [TestMethod]
        public void Add_Returns_1_When_Adding_Valid_RentalOrder()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                var rentalItemRepository = new RentalItemRepository(context);
                var userRepository = new UserRepository(context);
                var rentalOrderRepository = new RentalOrderRepository(context);
                var rentalOrderLineRepository = new RentalOrderLineRepository(context);

                //Act
                var item = ItemFactory.CreateValidEntity();
                itemRepository.Add(item);
                var rentalItem = RentalItemFactory.CreateValidEntity(item);
                rentalItemRepository.Add(rentalItem);
                var user = UserFactory.CreateValidEntity();
                userRepository.Add(user);
                var rentalOrder = RentalOrderFactory.CreateValidEntity(user);
                rentalOrderRepository.Add(rentalOrder);
                var rentalOrderLine = RentalOrderLineFactory.CreateValidEntity(rentalOrder, rentalItem);
                rentalOrderLineRepository.Add(rentalOrderLine);

                var result = context.SaveChanges();

                //Assert
                Assert.AreEqual(5, result); //Because we added five entities
            }
        }

        [TestMethod]
        public void Get_Returns_Null_When_Not_Found()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var rentalOrderRepository = new RentalOrderRepository(context);

                //Act
                var rentalOrder = rentalOrderRepository.Get(1);

                //Assert
                Assert.IsNull(rentalOrder);
            }
        }

        [TestMethod]
        public void Get_Returns_RentalOrder_When_Found()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                var rentalItemRepository = new RentalItemRepository(context);
                var userRepository = new UserRepository(context);
                var rentalOrderRepository = new RentalOrderRepository(context);
                var rentalOrderLineRepository = new RentalOrderLineRepository(context);

                var item = ItemFactory.CreateValidEntity();
                itemRepository.Add(item);
                var rentalItem = RentalItemFactory.CreateValidEntity(item);
                rentalItemRepository.Add(rentalItem);
                var user = UserFactory.CreateValidEntity();
                userRepository.Add(user);
                var rentalOrder = RentalOrderFactory.CreateValidEntity(user);
                rentalOrderRepository.Add(rentalOrder);
                var rentalOrderLineToAdd = RentalOrderLineFactory.CreateValidEntity(rentalOrder, rentalItem);
                rentalOrderLineRepository.Add(rentalOrderLineToAdd);

                context.SaveChanges();

                //Act
                var rentalOrderLine = rentalOrderLineRepository.Get(rentalOrderLineToAdd.Id);

                //Assert
                Assert.IsNotNull(rentalOrderLine);
            }
        }

        [TestMethod]
        public void GetAll_Returns_10_RentalOrders()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                var rentalItemRepository = new RentalItemRepository(context);
                var userRepository = new UserRepository(context);
                var rentalOrderRepository = new RentalOrderRepository(context);
                var rentalOrderLineRepository = new RentalOrderLineRepository(context);

                for (int i = 0; i < 10; i++)
                {
                    var item = ItemFactory.CreateValidEntity();
                    itemRepository.Add(item);
                    var rentalItem = RentalItemFactory.CreateValidEntity(item);
                    rentalItemRepository.Add(rentalItem);
                    var user = UserFactory.CreateValidEntity();
                    userRepository.Add(user);
                    var rentalOrder = RentalOrderFactory.CreateValidEntity(user);
                    rentalOrderRepository.Add(rentalOrder);
                    var rentalOrderLineToAdd = RentalOrderLineFactory.CreateValidEntity(rentalOrder, rentalItem);
                    rentalOrderLineRepository.Add(rentalOrderLineToAdd);
                }
                context.SaveChanges();

                //Act
                var rentalOrderLines = rentalOrderLineRepository.GetAll();

                //Assert
                Assert.AreEqual(10, rentalOrderLines.Count());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Remove_Throws_Exception_When_Not_Found()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                var rentalItemRepository = new RentalItemRepository(context);
                var userRepository = new UserRepository(context);
                var rentalOrderRepository = new RentalOrderRepository(context);
                var rentalOrderLineRepository = new RentalOrderLineRepository(context);

                var item = ItemFactory.CreateValidEntity();
                itemRepository.Add(item);
                var rentalItem = RentalItemFactory.CreateValidEntity(item);
                rentalItemRepository.Add(rentalItem);
                var user = UserFactory.CreateValidEntity();
                userRepository.Add(user);
                var rentalOrder = RentalOrderFactory.CreateValidEntity(user);
                rentalOrderRepository.Add(rentalOrder);
                var rentalOrderLineToAdd = RentalOrderLineFactory.CreateValidEntity(rentalOrder, rentalItem);
                rentalOrderLineRepository.Add(rentalOrderLineToAdd);

                context.SaveChanges();

                //Act
                rentalOrderLineRepository.Remove(RentalOrderLineFactory.CreateValidEntity(rentalOrder, rentalItem));
                context.SaveChanges();

                //Assert
            }
        }

        [TestMethod]
        public void Remove_Deletes_RentalOrder()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                var rentalItemRepository = new RentalItemRepository(context);
                var userRepository = new UserRepository(context);
                var rentalOrderRepository = new RentalOrderRepository(context);
                var rentalOrderLineRepository = new RentalOrderLineRepository(context);

                var item = ItemFactory.CreateValidEntity();
                itemRepository.Add(item);
                var rentalItem = RentalItemFactory.CreateValidEntity(item);
                rentalItemRepository.Add(rentalItem);
                var user = UserFactory.CreateValidEntity();
                userRepository.Add(user);
                var rentalOrder = RentalOrderFactory.CreateValidEntity(user);
                rentalOrderRepository.Add(rentalOrder);
                var rentalOrderLineToAdd = RentalOrderLineFactory.CreateValidEntity(rentalOrder, rentalItem);
                rentalOrderLineRepository.Add(rentalOrderLineToAdd);
                var rentalOrderLineId = rentalOrderLineToAdd.Id;
                context.SaveChanges();

                //Act
                rentalOrderLineRepository.Remove(rentalOrderLineToAdd);
                context.SaveChanges();

                var rentalOrderLine = rentalOrderRepository.Get(rentalOrderLineId);

                //Assert
                Assert.IsNull(rentalOrderLine);
            }
        }
    }
}
