using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VivesRental.Services.Exceptions
{
    class RentalItemReturnedException : Exception
    {
        public RentalItemReturnedException()
        {
            MessageBox.Show("Oops, that rental item is already returned");
        }

        public RentalItemReturnedException(string message)
            : base(message)
        {
        }

        public RentalItemReturnedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
