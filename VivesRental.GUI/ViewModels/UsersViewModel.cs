using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Messages;
using VivesRental.GUI.Services;
using VivesRental.Model;
using VivesRental.Services;

namespace VivesRental.GUI.ViewModels
{
    class UsersViewModel : ViewModelBase, IViewModel
    {

        private ObservableCollection<User> users = new ObservableCollection<User>();
        private bool _isLoading;

        public ICommand ViewUserCommand { get; private set; }
        public ObservableCollection<User> Users
        {
            get => users;
            private set
            {
                users = value;
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


        public UsersViewModel()
        {
            InstantiateCommands();
            GetUsers();
        }

        private void GetUsers()
        {
            IsLoading = true;
            var service = new UserService();
            Task.Run(() =>
            {
                Users = new ObservableCollection<User>(service.All());
                IsLoading = false;
            });
        }

        private void InstantiateCommands()
        {
            ViewUserCommand = new RelayCommand<Model.User>(OpenUserView);
        }

        private void OpenUserView(User user)
        {
            NavigationService.OpenView(new UserViewModel(user));
        }

        

    }
}
