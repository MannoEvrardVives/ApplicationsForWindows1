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
    public class RentalItemRepositoryTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            //Clear database
            DbContextHelper.Clear(new VivesRentalDbContext());
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void Add_Throws_Exception_When_Adding_InValid_Item()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var rentalItemRepository = new RentalItemRepository(context);

                //Act
                var rentalItem = RentalItemFactory.CreateInvalidEntity();
                rentalItemRepository.Add(rentalItem);

                var result = context.SaveChanges();

                //No Assert
            }
            
        }

        [TestMethod]
        public void Add_Returns_1_When_Adding_Valid_Item()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                var rentalItemRepository = new RentalItemRepository(context);

                //Act
                var item = ItemFactory.CreateValidEntity();
                itemRepository.Add(item);
                var rentalItem = RentalItemFactory.CreateValidEntity(item);
                rentalItemRepository.Add(rentalItem);

                var result = context.SaveChanges();

                //Assert
                Assert.AreEqual(2, result); //Because we added two entities
            }
        }

        [TestMethod]
        public void Get_Returns_Null_When_Not_Found()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var rentalItemRepository = new RentalItemRepository(context);

                //Act
                var rentalItem = rentalItemRepository.Get(1);

                //Assert
                Assert.IsNull(rentalItem);
            }
        }

        [TestMethod]
        public void Get_Returns_Item_When_Found()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                var rentalItemRepository = new RentalItemRepository(context);

                var item = ItemFactory.CreateValidEntity();
                itemRepository.Add(item);
                var rentalItemToAdd = RentalItemFactory.CreateValidEntity(item);
                rentalItemRepository.Add(rentalItemToAdd);
                context.SaveChanges();

                //Act
                var rentalItem = rentalItemRepository.Get(rentalItemToAdd.Id);

                //Assert
                Assert.IsNotNull(rentalItem);
            }
        }

        [TestMethod]
        public void GetAll_Returns_10_Items()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                var rentalItemRepository = new RentalItemRepository(context);
                for (int i = 0; i < 10; i++)
                {
                    var item = ItemFactory.CreateValidEntity();
                    itemRepository.Add(item);
                    var rentalItemToAdd = RentalItemFactory.CreateValidEntity(item);
                    rentalItemRepository.Add(rentalItemToAdd);
                }
                context.SaveChanges();

                //Act
                var rentalItems = rentalItemRepository.GetAll();

                //Assert
                Assert.AreEqual(10, rentalItems.Count());
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

                var item = ItemFactory.CreateValidEntity();
                itemRepository.Add(item);
                var rentalItemToAdd = RentalItemFactory.CreateValidEntity(item);
                rentalItemRepository.Add(rentalItemToAdd);
                
                context.SaveChanges();

                //Act
                rentalItemRepository.Remove(RentalItemFactory.CreateValidEntity(item));
                context.SaveChanges();

                //Assert
            }
        }

        [TestMethod]
        public void Remove_Deletes_Item()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                var rentalItemRepository = new RentalItemRepository(context);

                var item = ItemFactory.CreateValidEntity();
                itemRepository.Add(item);
                var rentalItemToAdd = RentalItemFactory.CreateValidEntity(item);
                rentalItemRepository.Add(rentalItemToAdd);
                var rentalItemId = rentalItemToAdd.Id;
                context.SaveChanges();

                //Act
                rentalItemRepository.Remove(rentalItemToAdd);
                context.SaveChanges();

                var rentalItem = rentalItemRepository.Get(rentalItemId);

                //Assert
                Assert.IsNull(rentalItem);
            }
        }
    }
}
