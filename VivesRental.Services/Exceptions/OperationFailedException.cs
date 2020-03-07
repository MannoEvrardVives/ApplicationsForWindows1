using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VivesRental.Services.Exceptions
{
    class OperationFailedException : Exception
    {
        public OperationFailedException()
        {
            MessageBox.Show("Oops, something went wrong, try again later.");
        }

        public OperationFailedException(string message)
            : base(message)
        {
        }

        public OperationFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}