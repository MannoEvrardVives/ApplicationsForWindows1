using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using VivesRental.GUI.Models;

namespace VivesRental.GUI.ViewModels
{
    class UserViewModel : ViewModelBase
    {
        private User user;

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                RaisePropertyChanged("User");
            }
        }
	}
}
