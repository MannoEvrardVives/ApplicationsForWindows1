using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Item> items = new ObservableCollection<Item>();
        private string[] possibleStatusList = Enum.GetNames(typeof(RentalItemStatus));
        private bool _isLoading;
        public ICommand EditRentalItemCommand { get; private set; }

        public ObservableCollection<Item> Items
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
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                RaisePropertyChanged();
            }
        }
        public ItemsManagementViewModel()
        {
            IsLoading = true;
            InstantiateCommands();
            var service = new ItemService();
            var include = new ItemIncludes
            {
                RentalItems = true
            };
            Task.Run(() =>
            {
                Items =  new ObservableCollection<Item>(service.All(include));
                IsLoading = false;
            });


        }

        private void InstantiateCommands()
        {
            EditRentalItemCommand = new RelayCommand(EditRentalItem);
        }

        private void EditRentalItem()
        {
            Debug.WriteLine("editing");
        }


    }
}
