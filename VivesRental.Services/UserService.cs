using System.Collections.Generic;
using System.Linq;
using VivesRental.Model;
using VivesRental.Services.Contracts;
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
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.Users.Get(id);
            }
        }

        public IList<User> All()
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                return unitOfWork.Users.GetAll().ToList();
            }
        }

        public User Create(User entity)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                if (!entity.IsValid())
                {
                    return null;
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

                return null;
            }
        }

        public User Edit(User entity)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                if (!entity.IsValid())
                {
                    return null;
                }

                //Get Item from unitOfWork
                var user = unitOfWork.Users.Get(entity.Id);
                if (user == null)
                {
                    return null;
                }

                //Only update the properties we want to update
                user.FirstName = entity.FirstName;
                user.Name = entity.Name;
                user.Email = entity.Email;
                user.PhoneNumber = entity.PhoneNumber;
                
                var numberOfObjectsUpdated = unitOfWork.Complete();
                if (numberOfObjectsUpdated > 0)
                    return entity;
                return null;
            }
        }

        public bool Remove(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.CreateInstance())
            {
                var user = unitOfWork.Users.Get(id);
                if (user == null)
                    return false;

                unitOfWork.Users.Remove(user);

                var numberOfObjectsUpdated = unitOfWork.Complete();
                return numberOfObjectsUpdated > 0;
            }
        }
    }
}
