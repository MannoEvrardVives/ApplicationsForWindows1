using System;
using VivesRental.Repository.Core;
using VivesRental.Services.Contracts;

namespace VivesRental.Services.Factories
{
	public class UnitOfWorkFactory: IUnitOfWorkFactory
	{
		public IUnitOfWork CreateInstance()
        {
            try
            {
                return new UnitOfWork();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
	}
}
