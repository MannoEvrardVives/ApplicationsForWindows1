using System.Collections.Generic;
using VivesRental.Model;

namespace VivesRental.Services.Contracts
{
    public interface IUserService
    {
        User Get(int id);
	    IList<User> All();
        User Create(User entity);
        User Edit(User entity);
        bool Remove(int id);
    }
}
