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
        private bool _isLoading;

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
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
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

            if (rentalOrderId == 0)
            {
                MessageBox.Show("¨Please enter a valid rental order id", "Return rental");
                return;
            }

            try
            {
                IsLoading = true;
                var service = new RentalOrderService();
                var rentalOrder = service.Get(RentalOrderId);
                IsLoading = false;
                NavigationService.OpenView(new RentalOrderDetailsViewModel(rentalOrder));
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }

        }

    }
}
