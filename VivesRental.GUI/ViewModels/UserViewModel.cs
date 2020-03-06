using System;
using GalaSoft.MvvmLight;
using VivesRental.Services;
using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Services;
using VivesRental.Model;


namespace VivesRental.GUI.ViewModels
{
    class UserViewModel : ViewModelBase, IViewModel
    {
        private User user = new User();
        private bool edit = false;

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

        public UserViewModel()
        {
            InstantiateCommands();
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
            var service = new UserService();
            var createdOrUpdateUser = edit ? service.Edit(User) : service.Create(User);

            if (createdOrUpdateUser == null)
            {
                Debug.WriteLine("Whoops, something went wrong");
            }
            else
            {
                NavigationService.OpenView(new UsersViewModel());
            }
        }
	}
}
