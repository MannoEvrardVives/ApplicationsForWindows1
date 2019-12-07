using System.Collections.Generic;
using VivesRental.Model;
using VivesRental.Repository.Core;

namespace VivesRental.Services.Contracts
{
    public interface IItemService
    {
        Item Get(int id);
        IList<Item> All();
        Item Create(Item entity);
        Item Edit(Item entity);
        bool Remove(int id);

    }
}
