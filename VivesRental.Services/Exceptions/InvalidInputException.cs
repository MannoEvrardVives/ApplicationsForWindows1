using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VivesRental.Services.Exceptions
{
    class InvalidInputException : Exception
    {
        public InvalidInputException()
        {
            MessageBox.Show("Oops, that input does not seem to be valid.");
        }

        public InvalidInputException(string message)
            : base(message)
        {
        }

        public InvalidInputException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
