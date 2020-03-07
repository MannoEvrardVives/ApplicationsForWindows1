using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VivesRental.Services.Exceptions
{
    class RentalItemNotFoundException : Exception
    {
        public RentalItemNotFoundException()
        {
            MessageBox.Show("Oops, we can't seem to find that rental item");
        }

        public RentalItemNotFoundException(string message)
            : base(message)
        {
        }

        public RentalItemNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
