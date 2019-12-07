using VivesRental.Repository.Core;
using VivesRental.Services.Contracts;

namespace VivesRental.Services.Factories
{
	public class UnitOfWorkFactory: IUnitOfWorkFactory
	{
		public IUnitOfWork CreateInstance()
		{
			return new UnitOfWork();
		}
	}
}
