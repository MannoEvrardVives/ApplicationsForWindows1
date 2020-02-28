using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VivesRental.GUI.Contracts;
using VivesRental.Model;
using VivesRental.Repository.Includes;
using VivesRental.Services;

namespace VivesRental.GUI.ViewModels
{
    class ItemsManagementViewModel : ViewModelBase, IViewModel
    {

        private List<Model.Item> items = new List<Model.Item>();
        private string[] possibleStatusList = Enum.GetNames(typeof(RentalItemStatus));


        public List<Model.Item> Items
        {
            get => items;
            private set
            {
                items = value;
                RaisePropertyChanged("Items");
            }
        }
        public string[] PossibleStatusList
        {
            get => possibleStatusList;
        }

        public ItemsManagementViewModel()
        {
            var service = new ItemService();
            var include = new ItemIncludes
            {
                RentalItems = true
            };
            Items = (List<Model.Item>)service.All(include);

        }


    }
}
