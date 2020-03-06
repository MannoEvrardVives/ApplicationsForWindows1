using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using VivesRental.GUI.Contracts;
using VivesRental.GUI.Messages;

namespace VivesRental.GUI.Services
{
    static class NavigationService
    {

        public static void OpenView(IViewModel viewModel)
        {
            var message = new NavigationMessage { ViewModel = viewModel };
            Messenger.Default.Send(message);
        }
    }
}
