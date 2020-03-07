using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VivesRental.Repository.Exceptions
{
    class NoDatabaseConnectionException : Exception
    {
        public NoDatabaseConnectionException()
        {
            MessageBox.Show("Oops, can't seem to connect to the database, try again later.");
        }

        public NoDatabaseConnectionException(string message)
            : base(message)
        {
        }

        public NoDatabaseConnectionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
