using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using VivesRental.Services;

namespace VivesRental.GUI.ViewModels
{
    class DashboardViewModel : ViewModelBase, IViewModel
    {

        private int rentalOrderId = 0;

        public ICommand OpenUsersManagementViewCommand { get; private set; }
        public ICommand OpenItemsManagementViewCommand { get; private set; }
        public ICommand OpenRentalManagementViewCommand { get; private set; }
        public ICommand OpenNewRentalViewCommand { get; private set; }
        public ICommand OpenReturnRentalViewCommand { get; private set; }

        public DashboardViewModel()
        {
            InstantiateCommands();
        }

        public int RentalOrderId
        {
            get => rentalOrderId;
            set
            {
                rentalOrderId = value;
                RaisePropertyChanged();
            }
        }

        private void InstantiateCommands()
        {
            OpenUsersManagementViewCommand = new RelayCommand(OpenUsersManagementView);
            OpenItemsManagementViewCommand = new RelayCommand(OpenItemsManagementView);
            OpenRentalManagementViewCommand = new RelayCommand(OpenRentalManagementView);
            OpenNewRentalViewCommand = new RelayCommand(OpenNewRentalView);
            OpenReturnRentalViewCommand = new RelayCommand(OpenReturnRentalView);
        }

        private void OpenUsersManagementView()
        {
            NavigationService.OpenView(new UsersViewModel());
        }
        private void OpenItemsManagementView()
        {
            NavigationService.OpenView(new ItemsManagementViewModel());
        }
        private void OpenRentalManagementView()
        {
            NavigationService.OpenView(new RentalOrdersViewModel());
        }
        private void OpenNewRentalView()
        {
            NavigationService.OpenView(new NewRentalViewModel());
        }
        private void OpenReturnRentalView()
        {
            if (rentalOrderId != 0)
            {
                var service = new RentalOrderService();
                var rentalOrder = service.Get(RentalOrderId);
                if (rentalOrder != null)
                {
                    NavigationService.OpenView(new RentalOrderDetailsViewModel(rentalOrder));
                }
                else Debug.WriteLine("Whoops, something went wrong");
            }
        }

    }
}
