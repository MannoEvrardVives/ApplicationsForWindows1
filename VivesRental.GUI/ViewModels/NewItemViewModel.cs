using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Services;
using VivesRental.Services;

namespace VivesRental.GUI.ViewModels
{
    class NewItemViewModel : ViewModelBase, IViewModel
    {

        private Model.Item item = new Model.Item();
        private int numberOfRentalItems = 0;
        private bool _isLoading;

        public ICommand CreateItemCommand { get; private set; }
        public Model.Item Item
        {
            get => item;
            set
            {
                item = value;
                RaisePropertyChanged();
            }
        }
        public int NumberOfRentalItems
        {
            get => numberOfRentalItems;
            set
            {
                numberOfRentalItems = value;
                RaisePropertyChanged();
            }
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
        public NewItemViewModel()
        {
            InstantiateCommands();
        }

        private void InstantiateCommands()
        {
            CreateItemCommand = new RelayCommand(CreateItem);
        }

        private void CreateItem()
        {
            IsLoading = true;
            Task.Run(() =>
            {
                try
                {
                    var service = new ItemService();
                    var createdItem = service.Create(Item);

                    var rentalItemService = new RentalItemService();

                    for (var i = 0; i < numberOfRentalItems; i++)
                    {
                        rentalItemService.Create(new Model.RentalItem(createdItem));
                    }

                    IsLoading = false;
                    NavigationService.OpenView(new ItemsManagementViewModel());
                }
                catch (Exception ex)
                {
                    IsLoading = false;
                }
            });

            
        }

    }
}
