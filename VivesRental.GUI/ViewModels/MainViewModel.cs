using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace VivesRental.GUI.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;


        #region Properties

        readonly static UserViewModel _userViewModel = new UserViewModel();
        readonly static DashboardViewModel _dashboardViewModel = new DashboardViewModel();

        public ICommand UserViewCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
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
        }

        private void InstantiateCommands()
        {
            UserViewCommand = new RelayCommand(ExecuteUserViewCommand);
            CloseCommand = new RelayCommand(ExecuteCloseCommand);
        }
        private void ExecuteUserViewCommand()
        {
            ShowViewModel(_userViewModel);
        }

        private void ShowViewModel(ViewModelBase viewModel)
        {
            CurrentViewModel = viewModel;
        }

        private void ExecuteCloseCommand()
        {
            Application.Current.Shutdown();
        }

    }
}