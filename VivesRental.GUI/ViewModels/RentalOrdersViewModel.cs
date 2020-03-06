using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Messages;
using VivesRental.GUI.Services;
using VivesRental.Model;
using VivesRental.Repository.Includes;
using VivesRental.Services;

namespace VivesRental.GUI.ViewModels
{
    class RentalOrdersViewModel : ViewModelBase, IViewModel
    {

        private ObservableCollection<RentalOrder> rentalOrders = new ObservableCollection<RentalOrder>();
        private bool _isLoading;
        public ICommand ViewRentalOrderCommand { get; private set; }
        public ObservableCollection<RentalOrder> RentalOrders
        {
            get => rentalOrders;
            private set
            {
                rentalOrders = value;
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

        public RentalOrdersViewModel()
        {
            IsLoading = true;
            InstantiateCommands();
            Task.Run(() =>
            {
                var service = new RentalOrderService();
                RentalOrders = new ObservableCollection<RentalOrder>(service.All());
                IsLoading = false;
            });


        }

        private void InstantiateCommands()
        {
            ViewRentalOrderCommand = new RelayCommand<Model.RentalOrder>(OpenRentalOrderView);
        }

        private void OpenRentalOrderView(RentalOrder chosenRentalOrder)
        {
            NavigationService.OpenView(new RentalOrderDetailsViewModel(chosenRentalOrder));
        }


    }
}
