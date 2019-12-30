using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using VivesRental.GUI.Models;
using System.Diagnostics;

namespace VivesRental.GUI.ViewModels
{
    class UserViewModel : ViewModelBase
    {
        private User user = new User();

       

        public User User
        {
            get
            {
                Debug.WriteLine("user: "+user.FirstName);
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
