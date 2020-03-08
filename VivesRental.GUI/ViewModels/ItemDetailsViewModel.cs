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
using VivesRental.GUI.Services;
using VivesRental.Model;
using VivesRental.Services;

namespace VivesRental.GUI.ViewModels
{
    class ItemDetailsViewModel : ViewModelBase, IViewModel
    {

        private Item item;
        private bool _isLoading;
        private string[] possibleStatusList = Enum.GetNames(typeof(RentalItemStatus));
        public ICommand EditRentalItemCommand { get; private set; }
        public ItemDetailsViewModel(Item item)
        {
            InstantiateCommands();
            Item = item;
        }

        public Item Item
        {
            get => item;
            private set
            {
                item = value;
                RaisePropertyChanged();
            }
        }
        public string[] PossibleStatusList
        {
            get => possibleStatusList;
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
            EditRentalItemCommand = new RelayCommand<RentalItem>(EditRentalItem);
        }

        private void EditRentalItem(RentalItem item)
        {
            _isLoading = true;
            try
            {
                var service = new RentalItemService();
                service.Edit(item);
                _isLoading = false;
            }
            catch (Exception)
            {
                _isLoading = false;
            }
        }

    }
}
