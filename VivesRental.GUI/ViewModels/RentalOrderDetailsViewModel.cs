using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private ObservableCollection<RentalOrderLine> rentalOrderLines = new ObservableCollection<RentalOrderLine>();
        private RentalOrder rentalOrder;
        private bool _isLoading;
        private readonly RentalOrderService service = new RentalOrderService();
        private readonly RentalOrderLineService rentalOrderLinesService = new RentalOrderLineService();
        public ICommand ReturnRentalOrderCommand { get; private set; }
        public ICommand ReturnRentalOrderLineCommand { get; private set; }

        public RentalOrderDetailsViewModel(RentalOrder rentalOrder)
        {
            InstantiateCommands();
            RentalOrder = rentalOrder;
            GetRentalOrderLines();
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
        public ObservableCollection<RentalOrderLine> RentalOrderLines
        {
            get => rentalOrderLines;
            private set
            {
                rentalOrderLines = value;
                RaisePropertyChanged();
            }
        }
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                RaisePropertyChanged();
            }
        }
        private void InstantiateCommands()
        {
            ReturnRentalOrderCommand = new RelayCommand(ReturnRentalOrder);
            ReturnRentalOrderLineCommand = new RelayCommand<RentalOrderLine>(ReturnRentalOrderLine);
        }

        private void GetRentalOrderLines()
        {
            IsLoading = true;
            Task.Run(() => {
                RentalOrderLines = new ObservableCollection<RentalOrderLine>(rentalOrderLinesService.FindByRentalOrderId(RentalOrder.Id));
                IsLoading = false;
            });
        }

        private void ReturnRentalOrder()
        {
            IsLoading = true;
            Task.Run(() =>
            {
                //TODO when returning rental item status should be set to normal
                var returned = service.Return(RentalOrder.Id, DateTime.Now);
                IsLoading = false;
                if (!returned)
                {
                    MessageBox.Show("Oops, something went wrong, try again later.", "Rental order details");
                    return;
                }

                GetRentalOrderLines();


            });
        }

        private void ReturnRentalOrderLine(RentalOrderLine rentalOrderLine)
        {
            IsLoading = true;
            Task.Run(() =>
            {
                // TODO Fix row has to be selected otherwise rentalOrderLine is null on button click
                var returned = rentalOrderLinesService.Return(rentalOrderLine.Id, DateTime.Now);
                IsLoading = false;
                if (!returned)
                {
                    MessageBox.Show("Oops, something went wrong, try again later.", "Rental order details");
                    return;
                }

                GetRentalOrderLines();


            });

        }

    }
}
