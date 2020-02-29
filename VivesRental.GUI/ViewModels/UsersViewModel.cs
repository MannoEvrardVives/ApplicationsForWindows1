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
using VivesRental.Model;
using VivesRental.Services;

namespace VivesRental.GUI.ViewModels
{
    class UsersViewModel : ViewModelBase, IViewModel
    {

        private List<Model.User> users = new List<Model.User>();

        public ICommand ViewUserCommand { get; private set; }
        public List<Model.User> Users
        {
            get => users;
            private set
            {
                users = value;
                RaisePropertyChanged();
            }
        }


        public UsersViewModel()
        {
            InstantiateCommands();
            var service = new UserService();
            Users = (List<User>)service.All();

        }

        private void InstantiateCommands()
        {
            ViewUserCommand = new RelayCommand<Model.User>(OpenUserView);
        }

        private void OpenUserView(User user)
        {
            OpenView(new UserViewModel(user));
        }

        private void OpenView(IViewModel viewModel)
        {
            var message = new NavigationMessage { ViewModel = viewModel };
            Messenger.Default.Send(message);
        }

    }
}
