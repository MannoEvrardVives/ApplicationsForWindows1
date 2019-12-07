using System.Data.Entity;
using VivesRental.Model;
using VivesRental.Repository.Contracts;
using VivesRental.Repository.Core;

namespace VivesRental.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public VivesRentalDbContext VivesRentalDbContext => Context as VivesRentalDbContext;
    }
}
