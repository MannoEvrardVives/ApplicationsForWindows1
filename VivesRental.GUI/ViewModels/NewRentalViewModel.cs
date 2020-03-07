using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Services;
using VivesRental.Model;
using VivesRental.Repository.Includes;
using VivesRental.Services;
using Item = VivesRental.Model.Item;

namespace VivesRental.GUI.ViewModels
{
    class NewRentalViewModel : ViewModelBase, IViewModel, INotifyPropertyChanged
    {

        private ObservableCollection<Model.Item> items = new ObservableCollection<Model.Item>();
        private ObservableCollection<Model.Item> rentedItems = new ObservableCollection<Model.Item>();
        private bool _isLoading;
        private int userId = 0;

        public ICommand AddRentalOrderCommand { get; private set; }
        public ICommand AddItemToRentCommand { get; private set; }
        public ObservableCollection<Model.Item> Items
        {
            get => items;
            private set
            {
                items = value;
                RaisePropertyChanged("Items");
            }
        }
        public ObservableCollection<Model.Item> RentedItems
        {
            get => rentedItems;
            private set
            {
                rentedItems = value;
                RaisePropertyChanged("RentedItems");
            }
        }
        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
                RaisePropertyChanged("UserId");
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
        public NewRentalViewModel()
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
                Items = new ObservableCollection<Item>(service.All(include));
                foreach (var item in Items)
                {
                    item.RentalItems = item.RentalItems
                        .Where(rentalItem => rentalItem.Status == RentalItemStatus.Normal).ToList();
                }

                IsLoading = false;
            });

        }

        private void InstantiateCommands()
        {
            AddRentalOrderCommand = new RelayCommand(AddRentalOrder);
            AddItemToRentCommand = new RelayCommand<Model.Item>(AddItemToRent);
        }


        private void AddItemToRent(Model.Item chosenItem)
        {

            if (chosenItem.RentalItems.Count == 0)
            {
                MessageBox.Show("Oops, there are no items available.", "Add new rental");
                return;
            }

            foreach (var item in rentedItems)
            {
                if (item.Id.Equals(chosenItem.Id))
                {
                    if (chosenItem.RentalItems.Count > 0)
                    {

                        Item updateItem = new Item
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            Manufacturer = item.Manufacturer,
                            Publisher = item.Publisher,
                            RentalExpiresAfterDays = item.RentalExpiresAfterDays,
                            RentalItems = item.RentalItems
                        };

                        updateItem.RentalItems.Add(chosenItem.RentalItems.First());
                        rentedItems.Remove(item);
                        rentedItems.Add(updateItem);

                        UpdateItemsOnSelection(chosenItem, 0);

                    }
                    else
                    {
                        MessageBox.Show("Oops, there are no items available.", "Add new rental");
                    }
                    return;
                }
                    
            }

            var newItem = new Item
            {
                Id = chosenItem.Id,
                Name = chosenItem.Name,
                Description = chosenItem.Description,
                Manufacturer = chosenItem.Manufacturer,
                Publisher = chosenItem.Publisher,
                RentalExpiresAfterDays = chosenItem.RentalExpiresAfterDays
            };
            newItem.RentalItems.Add(chosenItem.RentalItems.First());
            rentedItems.Add(newItem);

            UpdateItemsOnSelection(chosenItem, 0);
        }


        private void UpdateItemsOnSelection(Item chosenItem, int index)
        {
            chosenItem.RentalItems[index].Status = RentalItemStatus.Rented;

            Item updateChosenItem = new Item
            {
                Id = chosenItem.Id,
                Name = chosenItem.Name,
                Description = chosenItem.Description,
                Manufacturer = chosenItem.Manufacturer,
                Publisher = chosenItem.Publisher,
                RentalExpiresAfterDays = chosenItem.RentalExpiresAfterDays,
                RentalItems = chosenItem.RentalItems.Where(rentalItem => rentalItem.Status == RentalItemStatus.Normal).ToList()
            };

            items.Remove(chosenItem);
            items.Add(updateChosenItem);
        }


        private void AddRentalOrder()
        {
            if (rentedItems.Count == 0)
            {
                MessageBox.Show("Select at least one item to be rented out.", "Add new rental");
                return;
            }

            if (UserId == 0)
            {
                MessageBox.Show("Please enter a valid user id.", "Add new rental");
                return;
            }

            IsLoading = true;
            Task.Run(() =>
            {

                try
                {
                    var service = new RentalOrderService();
                    var rentalOrder = service.Create(UserId);

                    var rentalOrderLineService = new RentalOrderLineService();
                    var rentalItemService = new RentalItemService();
                    foreach (var rentedItem in RentedItems)
                    {
                        foreach (var rentalItem in rentedItem.RentalItems)
                        {
                            rentalOrderLineService.Rent(rentalOrder.Id, rentalItem.Id);
                            rentalItemService.Edit(rentalItem);
                        }

                    }
                    foreach (var item in Items)
                    {
                        foreach (var rentalItem in item.RentalItems)
                        {
                            rentalItemService.Edit(rentalItem);
                        }
                    }

                    IsLoading = false;
                    NavigationService.OpenView(new RentalOrdersViewModel());
                }
                catch (Exception ex)
                {
                    IsLoading = false;
                }
            });


        }


    }
}
