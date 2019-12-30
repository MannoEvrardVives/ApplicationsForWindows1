using System;
using GalaSoft.MvvmLight;
using VivesRental.GUI.Models;
using VivesRental.Services;
using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;


namespace VivesRental.GUI.ViewModels
{
    class UserViewModel : ViewModelBase
    {
        private User user = new User();

        public ICommand CreateUserCommand { get; private set; }


        public UserViewModel()
        {
            InstantiateCommands();
        }

        private void InstantiateCommands()
        {
            CreateUserCommand = new RelayCommand(CreateUser);
        }

        public User User
        {
            get => user;
            set
            {
                user = value;
                RaisePropertyChanged("User");
            }
        }

        public void CreateUser()
        {
            Debug.WriteLine("Clicked button");
            UserService service = new UserService();
            Model.User createdUser = service.Create(user);

            if (createdUser == null)
            {
                Debug.WriteLine("Whoops, something went wrong");
            }
            else Debug.WriteLine("Created user: " + createdUser);
        }
	}
}
