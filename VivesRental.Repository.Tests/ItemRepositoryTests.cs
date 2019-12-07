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
    public class ItemRepositoryTests
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
                var itemRepository = new ItemRepository(context);

                //Act
                var item = ItemFactory.CreateInvalidEntity();
                itemRepository.Add(item);

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
                var item = ItemFactory.CreateValidEntity();

                //Act
                itemRepository.Add(item);
                var result = context.SaveChanges();

                //Assert
                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void Get_Returns_Null_When_Not_Found()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);

                //Act
                var item = itemRepository.Get(1);

                //Assert
                Assert.IsNull(item);
            }
        }

        [TestMethod]
        public void Get_Returns_Item_When_Found()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                var itemToAdd = ItemFactory.CreateValidEntity();
                itemRepository.Add(itemToAdd);
                context.SaveChanges();

                //Act
                var item = itemRepository.Get(itemToAdd.Id);

                //Assert
                Assert.IsNotNull(item);
            }
        }

        [TestMethod]
        public void GetAll_Returns_10_Items()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var itemRepository = new ItemRepository(context);
                for (int i = 0; i < 10; i++)
                {
                    var itemToAdd = ItemFactory.CreateValidEntity();
                    itemRepository.Add(itemToAdd);
                }
                context.SaveChanges();

                //Act
                var items = itemRepository.GetAll();

                //Assert
                Assert.AreEqual(10, items.Count());
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
                
                var itemToAdd = ItemFactory.CreateValidEntity();
                itemRepository.Add(itemToAdd);
                
                context.SaveChanges();

                //Act
                itemRepository.Remove(ItemFactory.CreateValidEntity());
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

                var itemToAdd = ItemFactory.CreateValidEntity();
                itemRepository.Add(itemToAdd);
                context.SaveChanges();
	            var itemId = itemToAdd.Id;

				//Act
				itemRepository.Remove(itemToAdd);
                context.SaveChanges();

                var item = itemRepository.Get(itemId);

                //Assert
                Assert.IsNull(item);
            }
        }

	    
	}
}
