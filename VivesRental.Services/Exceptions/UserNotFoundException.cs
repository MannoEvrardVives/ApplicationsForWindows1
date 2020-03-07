using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VivesRental.Services.Exceptions
{
    class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {
            MessageBox.Show("Oops, we can't seem to find that user");
        }

        public UserNotFoundException(string message)
            : base(message)
        {
        }

        public UserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
