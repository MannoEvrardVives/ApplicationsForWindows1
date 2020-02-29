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
using VivesRental.Model;
using VivesRental.Repository.Includes;
using VivesRental.Services;

namespace VivesRental.GUI.ViewModels
{
    class RentalOrdersViewModel : ViewModelBase, IViewModel
    {

        private List<Model.RentalOrder> rentalOrders = new List<Model.RentalOrder>();
        
        public ICommand ViewRentalOrderCommand { get; private set; }
        public List<Model.RentalOrder> RentalOrders
        {
            get => rentalOrders;
            private set
            {
                rentalOrders = value;
                RaisePropertyChanged();
            }
        }
        

        public RentalOrdersViewModel()
        {
            InstantiateCommands();

            var service = new RentalOrderService();
            RentalOrders = (List<RentalOrder>)service.All();

        }

        private void InstantiateCommands()
        {
            ViewRentalOrderCommand = new RelayCommand<Model.RentalOrder>(OpenRentalOrderView);
        }

        private void OpenRentalOrderView(RentalOrder chosenRentalOrder)
        {
            OpenView(new RentalOrderDetailsViewModel(chosenRentalOrder));
        }

        private void OpenView(IViewModel viewModel)
        {
            var message = new NavigationMessage { ViewModel = viewModel };
            Messenger.Default.Send(message);
        }

    }
}
