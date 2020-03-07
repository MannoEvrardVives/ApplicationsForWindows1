using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VivesRental.Services.Exceptions
{
    class RentalOrderLineNotFoundException:Exception
    {
        public RentalOrderLineNotFoundException()
        {
            MessageBox.Show("Oops, we can't seem to find that rental order line");
        }

        public RentalOrderLineNotFoundException(string message)
            : base(message)
        {
        }

        public RentalOrderLineNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
