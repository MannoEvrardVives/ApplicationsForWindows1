using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VivesRental.GUI.Contracts;
using VivesRental.Model;
using VivesRental.Services;

namespace VivesRental.GUI.ViewModels
{
    class RentalOrderDetailsViewModel : ViewModelBase, IViewModel
    {

        private RentalOrder rentalOrder;
        private readonly RentalOrderService service = new RentalOrderService();
        private readonly RentalOrderLineService rentalOrderLinesService = new RentalOrderLineService();
        public ICommand ReturnRentalOrderCommand { get; private set; }
        public ICommand ReturnRentalOrderLineCommand { get; private set; }

        public RentalOrderDetailsViewModel(RentalOrder rentalOrder)
        {
            InstantiateCommands();
            RentalOrder = rentalOrder;
            RentalOrder.RentalOrderLines = rentalOrderLinesService.FindByRentalOrderId(RentalOrder.Id);
        }

        public RentalOrder RentalOrder
        {
            get => rentalOrder;
            private set
            {
                rentalOrder = value;
                RaisePropertyChanged();
            }
        }

        private void InstantiateCommands()
        {
            ReturnRentalOrderCommand = new RelayCommand(ReturnRentalOrder);
            ReturnRentalOrderLineCommand = new RelayCommand<RentalOrderLine>(ReturnRentalOrderLine);
        }

        private void ReturnRentalOrder()
        {
            //TODO when returning rental item status should be set to normal
            var returned = service.Return(RentalOrder.Id, DateTime.Now);
            if (!returned)
            {
                Debug.WriteLine("Whoops, something went wrong");
            }
            else Debug.WriteLine("returned");
        }

        private void ReturnRentalOrderLine(RentalOrderLine rentalOrderLine)
        {
            var returned = rentalOrderLinesService.Return(rentalOrderLine.Id, DateTime.Now);
            if (!returned)
            {
                Debug.WriteLine("Whoops, something went wrong");
            }
            else Debug.WriteLine("returned");
        }

    }
}
