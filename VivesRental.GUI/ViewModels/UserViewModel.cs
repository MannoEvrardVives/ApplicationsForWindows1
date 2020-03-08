using System;
using GalaSoft.MvvmLight;
using VivesRental.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Command;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Services;
using VivesRental.Model;


namespace VivesRental.GUI.ViewModels
{
    class UserViewModel : ViewModelBase, IViewModel
    {
        private User user;
        private readonly bool edit = false;
        private bool _isLoading;
        public ICommand CreateUserCommand { get; private set; }
        public User User
        {
            get => user;
            set
            {
                user = value;
                RaisePropertyChanged("User");
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
        public UserViewModel()
        {
            InstantiateCommands();
            User = new User();
        }

        public UserViewModel(User user)
        {
            InstantiateCommands();
            User = user;
            edit = true;
        }

        private void InstantiateCommands()
        {
            CreateUserCommand = new RelayCommand(CreateUser);
        }

        private void CreateUser()
        {
            IsLoading = true;
            Task.Run(() =>
            {
                try
                {
                    var service = new UserService();
                    var createdOrUpdateUser = edit ? service.Edit(User) : service.Create(User);
                    IsLoading = false;
                    NavigationService.OpenView(new UsersViewModel());
                }
                catch (Exception ex)
                {
                    IsLoading = false;
                }
                
            });
            
            
        }
	}
}
