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
    public class RentalOrderRepositoryTests
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
                var rentalOrderRepository = new RentalOrderRepository(context);

                //Act
                var rentalOrder = RentalOrderFactory.CreateInvalidEntity();
                rentalOrderRepository.Add(rentalOrder);

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
                var userRepository = new UserRepository(context);
                var rentalOrderRepository = new RentalOrderRepository(context);

                //Act
                var user = UserFactory.CreateValidEntity();
                userRepository.Add(user);
                var rentalOrder = RentalOrderFactory.CreateValidEntity(user);
                rentalOrderRepository.Add(rentalOrder);

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
                var userRepository = new UserRepository(context);
                var rentalOrderRepository = new RentalOrderRepository(context);

                var user = UserFactory.CreateValidEntity();
                userRepository.Add(user);
                var rentalOrderToAdd = RentalOrderFactory.CreateValidEntity(user);
                rentalOrderRepository.Add(rentalOrderToAdd);

                context.SaveChanges();

                //Act
                var rentalOrder = rentalOrderRepository.Get(rentalOrderToAdd.Id);

                //Assert
                Assert.IsNotNull(rentalOrder);
            }
        }

        [TestMethod]
        public void GetAll_Returns_10_RentalOrders()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var userRepository = new UserRepository(context);
                var rentalOrderRepository = new RentalOrderRepository(context);
                for (int i = 0; i < 10; i++)
                {
                    var user = UserFactory.CreateValidEntity();
                    userRepository.Add(user);
                    var rentalOrderToAdd = RentalOrderFactory.CreateValidEntity(user);
                    rentalOrderRepository.Add(rentalOrderToAdd);
                }
                context.SaveChanges();

                //Act
                var rentalOrders = rentalOrderRepository.GetAll();

                //Assert
                Assert.AreEqual(10, rentalOrders.Count());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Remove_Throws_Exception_When_Not_Found()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var userRepository = new UserRepository(context);
                var rentalOrderRepository = new RentalOrderRepository(context);

                var user = UserFactory.CreateValidEntity();
                userRepository.Add(user);
                var rentalOrderToAdd = RentalOrderFactory.CreateValidEntity(user);
                rentalOrderRepository.Add(rentalOrderToAdd);
                
                context.SaveChanges();

                //Act
                rentalOrderRepository.Remove(RentalOrderFactory.CreateValidEntity(user));
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
                var userRepository = new UserRepository(context);
                var rentalOrderRepository = new RentalOrderRepository(context);

                var user = UserFactory.CreateValidEntity();
                userRepository.Add(user);
                var rentalOrderToAdd = RentalOrderFactory.CreateValidEntity(user);
                rentalOrderRepository.Add(rentalOrderToAdd);
                var rentalOrderId = rentalOrderToAdd.Id;
                context.SaveChanges();

                //Act
                rentalOrderRepository.Remove(rentalOrderToAdd);
                context.SaveChanges();

                var rentalOrder = rentalOrderRepository.Get(rentalOrderId);

                //Assert
                Assert.IsNull(rentalOrder);
            }
        }
    }
}
