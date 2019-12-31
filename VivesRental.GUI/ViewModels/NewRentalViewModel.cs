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
using VivesRental.Model;

namespace VivesRental.GUI.ViewModels
{
    class NewRentalViewModel : ViewModelBase, IViewModel, INotifyPropertyChanged
    {

        private List<Model.Item> items = new List<Model.Item>();
        private ObservableCollection<Model.RentalItem> rentalItems = new ObservableCollection<Model.RentalItem>();

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
        public ObservableCollection<Model.RentalItem> RentalItems
        {
            get => rentalItems;
            private set
            {
                rentalItems = value;
                RaisePropertyChanged("RentalItems");
            }
        }

        public NewRentalViewModel()
        {
            InstantiateCommands();

            //temp
            for (var i = 1; i <= 10; i++)
            {
                items.Add(new Item());
            }
        }

        private void InstantiateCommands()
        {
            AddRentalOrderCommand = new RelayCommand(AddRentalOrder);
            AddItemToRentCommand = new RelayCommand<Model.Item>(AddItemToRent);
        }


        private void AddItemToRent(Model.Item item)
        {
            rentalItems.Add(new Models.RentalItem(item));
            Debug.WriteLine("rental items: "+rentalItems.Count);
        }
        private void AddRentalOrder()
        {

        }


    }
}
