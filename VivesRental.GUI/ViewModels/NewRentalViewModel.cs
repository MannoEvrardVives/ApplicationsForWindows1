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
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Models;
using VivesRental.Model;
using VivesRental.Repository.Includes;
using VivesRental.Services;
using Item = VivesRental.Model.Item;
using RentalItem = VivesRental.Model.RentalItem;
using RentalOrder = VivesRental.GUI.Models.RentalOrder;

namespace VivesRental.GUI.ViewModels
{
    class NewRentalViewModel : ViewModelBase, IViewModel, INotifyPropertyChanged
    {

        private List<Model.Item> items = new List<Model.Item>();
        private ObservableCollection<Model.Item> rentedItems = new ObservableCollection<Model.Item>();
        private int userId = 0;

        public ICommand AddRentalOrderCommand { get; private set; }
        public ICommand AddItemToRentCommand { get; private set; }
        public List<Model.Item> Items
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

        public NewRentalViewModel()
        {
            InstantiateCommands();

            var service = new ItemService();
            var include = new ItemIncludes
            {
                RentalItems = true
            };
            Items = (List<Model.Item>)service.All(include);
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
                //TODO: show message saying there are no rentalItems available
                Debug.WriteLine("No items to be rented");
                return;
            }

            foreach (var item in rentedItems)
            {
                if (item.Id.Equals(chosenItem.Id))
                {
                    if (item.RentalItems.Count < chosenItem.RentalItems.Count)
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

                        updateItem.RentalItems.Add(chosenItem.RentalItems[updateItem.RentalItems.Count]);
                        rentedItems.Remove(item);
                        rentedItems.Add(updateItem);

                    }
                    else
                    {
                        Debug.WriteLine("No items to be rented anymore, out of stock");
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
        }

        private void AddRentalOrder()
        {
            if (rentedItems.Count == 0)
            {
                //TODO: show message saying there are no items selected
                Debug.WriteLine("No items to be rented");
                return;
            }

            var service = new RentalOrderService();
            var rentalOrder = service.Create(UserId);

            if (rentalOrder != null)
            {
                var rentalOrderLineService = new RentalOrderLineService();
                foreach (var rentedItem in RentedItems)
                {
                    foreach (var rentalItem in rentedItem.RentalItems)
                    {
                        rentalOrderLineService.Rent(rentalOrder.Id, rentalItem.Id);
                    }
                }
            }
            else Debug.WriteLine("Whoops, something went wrong");


        }


    }
}
