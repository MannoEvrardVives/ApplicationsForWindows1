using System;
using GalaSoft.MvvmLight;
using VivesRental.GUI.Models;
using VivesRental.Services;
using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using VivesRental.GUI.Contracts;


namespace VivesRental.GUI.ViewModels
{
    class UserViewModel : ViewModelBase, IViewModel
    {
        private User user = new User();

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

        private void InstantiateCommands()
        {
            CreateUserCommand = new RelayCommand(CreateUser);
        }

        private void CreateUser()
        {
            var service = new UserService();
            var createdUser = service.Create(user);

            if (createdUser == null)
            {
                Debug.WriteLine("Whoops, something went wrong");
            }
            else Debug.WriteLine("Created user: " + createdUser);
        }
	}
}
