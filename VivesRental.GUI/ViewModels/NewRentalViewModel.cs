using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VivesRental.Services;
using RentalOrder = VivesRental.GUI.Models.RentalOrder;

namespace VivesRental.GUI.ViewModels
{
    class NewRentalViewModel : ViewModelBase, IViewModel, INotifyPropertyChanged
    {

        private List<Model.Item> items = new List<Model.Item>();
        private ObservableCollection<Models.Item> rentedItems = new ObservableCollection<Models.Item>();

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
        public ObservableCollection<Models.Item> RentedItems
        {
            get => rentedItems;
            private set
            {
                rentedItems = value;
                RaisePropertyChanged("RentedItems");
            }
        }

        public NewRentalViewModel()
        {
            InstantiateCommands();

            //temp
            for (var i = 1; i <= 10; i++)
            {
                //items.Add(new Model.Item(i));
            }
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
                    Models.Item itemCopy = new Models.Item(chosenItem)
                    {
                        RentalItems = item.RentalItems
                    };
                    itemCopy.RentalItems.Add(chosenItem.RentalItems.Last());
                    rentedItems.Add(itemCopy);
                    rentedItems.Remove(item);
                    return;
                }
                    
            }

            Models.Item newItem = new Models.Item(chosenItem);
            newItem.RentalItems.Add(chosenItem.RentalItems.Last());
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
            RentalOrder order = new RentalOrder(0, rentedItems);
            //TODO: do something with new order
        }


    }
}
