using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Messages;

namespace VivesRental.GUI.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private IViewModel _currentViewModel;


        #region Properties

        readonly static UserViewModel _userViewModel = new UserViewModel();
        readonly static DashboardViewModel _dashboardViewModel = new DashboardViewModel();

        public ICommand DashboardViewCommand { get; private set; }
        public ICommand UserViewCommand { get; private set; }
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
            CurrentViewModel = MainViewModel._dashboardViewModel;
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