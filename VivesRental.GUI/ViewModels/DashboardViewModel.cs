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

        public ICommand OpenUsersManagementCommand { get; private set; }
        public ICommand OpenItemsManagementCommand { get; private set; }
        public ICommand OpenRentalManagementCommand { get; private set; }

        public DashboardViewModel()
        {
            InstantiateCommands();
        }

        private void InstantiateCommands()
        {
            OpenUsersManagementCommand = new RelayCommand(OpenUsersManagement);
            OpenItemsManagementCommand = new RelayCommand(OpenItemsManagement);
            OpenRentalManagementCommand = new RelayCommand(OpenRentalManagement);
        }

        private void OpenUsersManagement()
        {
            OpenModelView(new UserViewModel());
        }
        private void OpenItemsManagement()
        {

        }
        private void OpenRentalManagement()
        {

        }

        private void OpenModelView(IViewModel viewModel)
        {
            var message = new NavigationMessage { ViewModel = viewModel };
            Messenger.Default.Send(message);
        }
    }
}
