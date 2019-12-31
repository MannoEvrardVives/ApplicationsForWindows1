using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Messages;

namespace VivesRental.GUI.ViewModels
{
    class DashboardViewModel : ViewModelBase, IViewModel
    {

        public ICommand OpenUsersManagementViewCommand { get; private set; }
        public ICommand OpenItemsManagementViewCommand { get; private set; }
        public ICommand OpenRentalManagementViewCommand { get; private set; }
        public ICommand OpenNewRentalViewCommand { get; private set; }
        public ICommand OpenReturnRentalViewCommand { get; private set; }

        public DashboardViewModel()
        {
            InstantiateCommands();
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
            OpenView(new UserViewModel());
        }
        private void OpenItemsManagementView()
        {

        }
        private void OpenRentalManagementView()
        {

        }
        private void OpenNewRentalView()
        {

        }
        private void OpenReturnRentalView()
        {

        }

        private void OpenView(IViewModel viewModel)
        {
            var message = new NavigationMessage { ViewModel = viewModel };
            Messenger.Default.Send(message);
        }
    }
}
