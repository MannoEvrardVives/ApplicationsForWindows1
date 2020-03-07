using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VivesRental.Model;
using VivesRental.Services.Contracts;
using VivesRental.Services.Exceptions;
using VivesRental.Services.Extensions;
using VivesRental.Services.Factories;

namespace VivesRental.Services
{
    public class UserService: IUserService
    {
		private readonly IUnitOfWorkFactory unitOfWorkFactory;

	    public UserService()
	    {
		    this.unitOfWorkFactory = new UnitOfWorkFactory();
	    }

	    public UserService(IUnitOfWorkFactory unitOfWorkFactory)
	    {
		    this.unitOfWorkFactory = unitOfWorkFactory;
	    }

		public User Get(int id)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.Users.Get(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<User> All()
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    return unitOfWork.Users.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User Create(User entity)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    if (!entity.IsValid())
                    {
                        throw new InvalidInputException();
                    }

                    var user = new User
                    {
                        FirstName = entity.FirstName,
                        Name = entity.Name,
                        Email = entity.Email,
                        PhoneNumber = entity.PhoneNumber
                    };

                    unitOfWork.Users.Add(user);
                    var numberOfObjectsUpdated = unitOfWork.Complete();

                    if (numberOfObjectsUpdated > 0)
                        return user;

                    throw new OperationFailedException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public User Edit(User entity)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    if (!entity.IsValid())
                    {
                        throw new InvalidInputException();
                    }

                    //Get Item from unitOfWork
                    var user = unitOfWork.Users.Get(entity.Id);
                    if (user == null)
                    {
                       throw new UserNotFoundException();
                    }

                    //Only update the properties we want to update
                    user.FirstName = entity.FirstName;
                    user.Name = entity.Name;
                    user.Email = entity.Email;
                    user.PhoneNumber = entity.PhoneNumber;

                    var numberOfObjectsUpdated = unitOfWork.Complete();
                    if (numberOfObjectsUpdated > 0)
                        return entity;
                    throw new OperationFailedException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool Remove(int id)
        {
            try
            {
                using (var unitOfWork = unitOfWorkFactory.CreateInstance())
                {
                    var user = unitOfWork.Users.Get(id);
                    if (user == null)
                        return false;

                    unitOfWork.Users.Remove(user);

                    var numberOfObjectsUpdated = unitOfWork.Complete();
                    if (numberOfObjectsUpdated > 0)
                        return true;
                    throw new OperationFailedException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
