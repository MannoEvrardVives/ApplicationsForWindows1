using VivesRental.Repository.Core;

namespace VivesRental.Services.Contracts
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork CreateInstance();
	}
}
