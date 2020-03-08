using System.Windows;
using System.Windows.Input;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Messages;
using WPFNotification.Model;
using WPFNotification.Services;

namespace VivesRental.GUI.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private IViewModel _currentViewModel;

        #region Properties

        readonly static UserViewModel _userViewModel = new UserViewModel();
        readonly static DashboardViewModel _dashboardViewModel = new DashboardViewModel();
        readonly static NewItemViewModel _newItemViewModel = new NewItemViewModel();
        readonly static ItemsManagementViewModel _itemsManagementViewModel = new ItemsManagementViewModel();
        readonly static NewRentalViewModel _newRentalViewModel = new NewRentalViewModel();
        readonly static RentalOrdersViewModel _rentalOrdersViewModel = new RentalOrdersViewModel();
        readonly static UsersViewModel _usersViewModel = new UsersViewModel();

        public ICommand DashboardViewCommand { get; private set; }
        public ICommand UserViewCommand { get; private set; }
        public ICommand NewItemViewCommand { get; private set; }
        public ICommand ItemsManagementViewCommand { get; private set; }
        public ICommand NewRentalViewCommand { get; private set; }
        public ICommand RentalOrdersViewCommand { get; private set; }
        public ICommand UsersViewCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        public IViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        #endregion

        public MainViewModel()
        {
            CurrentViewModel = _dashboardViewModel;
            InstantiateCommands();
            InitializeMessenger();
        }

        private void InitializeMessenger()
        {
            Messenger.Default.Register<NavigationMessage>(this, NavigateToViewModel);
        }
        private void InstantiateCommands()
        {
            DashboardViewCommand = new RelayCommand(ExecuteDashboardViewCommand);
            UserViewCommand = new RelayCommand(ExecuteUserViewCommand);
            NewItemViewCommand = new RelayCommand(ExecuteNewItemViewCommand);
            RentalOrdersViewCommand = new RelayCommand(ExecuteRentalOrdersViewCommand);
            ItemsManagementViewCommand = new RelayCommand(ExecuteItemsManagementViewCommand);
            NewRentalViewCommand = new RelayCommand(ExecuteNewRentalViewCommand);
            UsersViewCommand = new RelayCommand(ExecuteUsersViewCommand);
            CloseCommand = new RelayCommand(ExecuteCloseCommand);
        }
        private void ExecuteDashboardViewCommand()
        {
            ShowViewModel(_dashboardViewModel);
        }
        private void ExecuteUserViewCommand()
        {
            ShowViewModel(_userViewModel);
        }
        private void ExecuteNewItemViewCommand()
        {
            ShowViewModel(_newItemViewModel);
        }
        private void ExecuteNewRentalViewCommand()
        {
            ShowViewModel(_newRentalViewModel);
        }
        private void ExecuteUsersViewCommand()
        {
            ShowViewModel(_usersViewModel);
        }
        private void ExecuteItemsManagementViewCommand()
        {
            ShowViewModel(_itemsManagementViewModel);
        }
        private void ExecuteRentalOrdersViewCommand()
        {
            ShowViewModel(_rentalOrdersViewModel);
        }

        private void ShowViewModel(IViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }
        private void NavigateToViewModel(NavigationMessage message)
        {
            CurrentViewModel = message.ViewModel;
        }

        private void ExecuteCloseCommand()
        {
            Application.Current.Shutdown();
        }

    }
}