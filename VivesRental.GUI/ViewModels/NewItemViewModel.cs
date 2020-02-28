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
using VivesRental.GUI.Models;
using VivesRental.Services;

namespace VivesRental.GUI.ViewModels
{
    class NewItemViewModel : ViewModelBase, IViewModel
    {

        private Model.Item item = new Model.Item();

        public ICommand CreateItemCommand { get; private set; }
        public Model.Item Item
        {
            get => item;
            set
            {
                item = value;
                RaisePropertyChanged();
            }
        }

        public NewItemViewModel()
        {
            InstantiateCommands();
        }

        private void InstantiateCommands()
        {
            CreateItemCommand = new RelayCommand(CreateItem);
        }

        private void CreateItem()
        {
            var service = new ItemService();
            var createdItem = service.Create(Item);

            if (createdItem == null)
            {
                Debug.WriteLine("Whoops, something went wrong");
            }
            else Debug.WriteLine("Created item: " + createdItem);
        }

    }
}
