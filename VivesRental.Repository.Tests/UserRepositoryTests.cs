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
    public class UserRepositoryTests
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
                var userRepository = new UserRepository(context);

                //Act
                var user = UserFactory.CreateInvalidEntity();
                userRepository.Add(user);

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
                var userRepository = new UserRepository(context);

                //Act
                var user = UserFactory.CreateValidEntity();
                userRepository.Add(user);

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
                var userRepository = new UserRepository(context);

                //Act
                var user = userRepository.Get(1);

                //Assert
                Assert.IsNull(user);
            }
        }

        [TestMethod]
        public void Get_Returns_Item_When_Found()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var userRepository = new UserRepository(context);
                var userToAdd = UserFactory.CreateValidEntity();
                userRepository.Add(userToAdd);
                context.SaveChanges();

                //Act
                var user = userRepository.Get(userToAdd.Id);

                //Assert
                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        public void GetAll_Returns_10_Items()
        {
            using (var context = new VivesRentalDbContext())
            {
                //Arrange
                var userRepository = new UserRepository(context);
                for (int i = 0; i < 10; i++)
                {
                    var userToAdd = UserFactory.CreateValidEntity();
                    userRepository.Add(userToAdd);
                }
                context.SaveChanges();

                //Act
                var users = userRepository.GetAll();

                //Assert
                Assert.AreEqual(10, users.Count());
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
                
                var userToAdd = UserFactory.CreateValidEntity();
                userRepository.Add(userToAdd);
                
                context.SaveChanges();

                //Act
                userRepository.Remove(UserFactory.CreateValidEntity());
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
                var userRepository = new UserRepository(context);

                var userToAdd = UserFactory.CreateValidEntity();
                userRepository.Add(userToAdd);
                var userId = userToAdd.Id;
                context.SaveChanges();

                //Act
                userRepository.Remove(userToAdd);
                context.SaveChanges();

                var user = userRepository.Get(userId);

                //Assert
                Assert.IsNull(user);
            }
        }
    }
}
