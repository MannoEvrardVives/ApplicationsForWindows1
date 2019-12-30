using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VivesRental.GUI.Models
{
    class User : INotifyPropertyChanged
    {

        #region Members

        private int id;
        private string firstName = "Manno";
        private string name = "Evrard";
        private string email = "manno.evrard@gmail.com";
        private string phoneNumber = "+32471178519";

        #endregion

        #region Properties

        public int Id
        {
            get => id;
            private set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
}
