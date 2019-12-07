using System;
using VivesRental.Model;
using VivesRental.Services;

namespace VivesRental.Tests.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Integration Tests
            ItemServiceCreateFailedTest();
            ItemServiceCreateSucceededTest();
            ItemServiceEditFailedTest();
            ItemServiceEditSucceededTest();
            ItemServiceRemoveFailedTest();
            ItemServiceRemoveSucceededTest();
            Console.ReadLine();
        }

        #region ItemService

        private static void ItemServiceCreateFailedTest()
        {
            WriteTitle("ItemService Create Failed Test");

            var itemService = new ItemService();
            var item = new Item
            {
                Manufacturer = "TestManufacturer",
                RentalExpiresAfterDays = 2
            };

            WriteSubTitle("Create");
            var result = itemService.Create(item);

            if (result != null)
                WriteMessage("Succeeded");
            else
                WriteMessage("Failed");

        }

        private static void ItemServiceCreateSucceededTest()
        {
            WriteTitle("ItemService Create Succeeded Test");

            var itemService = new ItemService();
            var item = new Item
            {
                Name = "TestName",
                Manufacturer = "TestManufacturer",
                RentalExpiresAfterDays = 2
            };

            WriteSubTitle("Create");
            var result = itemService.Create(item);

            if (result != null)
                WriteMessage("Succeeded");
            else
                WriteMessage("Failed");

        }

        private static void ItemServiceEditFailedTest()
        {
            WriteTitle("ItemService Edit Failed Test");

            var itemService = new ItemService();
            var item = new Item
            {
                Manufacturer = "TestManufacturer",
                RentalExpiresAfterDays = 2
            };

            WriteSubTitle("Edit");
            var result = itemService.Edit(item);

            if (result != null)
                WriteMessage("Succeeded");
            else
                WriteMessage("Failed");

        }

        private static void ItemServiceEditSucceededTest()
        {
            WriteTitle("ItemService Edit Succeeded Test");

            var itemService = new ItemService();
            var item = new Item
            {
                Name = "TestName",
                Manufacturer = "TestManufacturer",
                RentalExpiresAfterDays = 2
            };

            WriteSubTitle("Create");
            var result = itemService.Create(item);

            if (result != null)
                WriteMessage("Succeeded");
            else
                WriteMessage("Failed");

        }

        private static void ItemServiceRemoveFailedTest()
        {
            WriteTitle("ItemService Remove Succeeded Test");

            var itemService = new ItemService();
            var item = new Item
            {
                Name = "TestName",
                Manufacturer = "TestManufacturer",
                RentalExpiresAfterDays = 2
            };

            var result = itemService.Create(item);
            if (result != null)
            {
                WriteMessage("Create Succeeded");

                WriteSubTitle("Remove");
                var removeResult = itemService.Remove(item.Id + 1); //Won't exist

                if (removeResult)
                    WriteMessage("Remove Succeeded");
                else
                    WriteMessage("Remove Failed");
            }
            else
            {
                WriteMessage("Create Failed");
            }
        }

        private static void ItemServiceRemoveSucceededTest()
        {
            WriteTitle("ItemService Remove Succeeded Test");

            var itemService = new ItemService();
            var item = new Item
            {
                Name = "TestName",
                Manufacturer = "TestManufacturer",
                RentalExpiresAfterDays = 2
            };

            var result = itemService.Create(item);
            if (result != null)
            {
                WriteMessage("Create Succeeded");

                WriteSubTitle("Remove");
                var removeResult = itemService.Remove(item.Id);

                if (removeResult)
                    WriteMessage("Remove Succeeded");
                else
                    WriteMessage("Remove Failed");
            }
            else
            {
                WriteMessage("Create Failed");
            }
            
        }

        #endregion


        private static void WriteTitle(string title)
        {
            //Write empty line
            Console.WriteLine();

            //Writes a dashed line, the same length as the title
            Console.WriteLine(new String('=', title.Length));
            //Write the title
            Console.WriteLine(title);
            //Writes a dashed line, the same length as the title
            Console.WriteLine(new String('=', title.Length));
        }

        private static void WriteSubTitle(string subTitle)
        {
            //Write empty line
            Console.WriteLine();

            //Write the title
            Console.WriteLine(subTitle);
            //Writes a dashed line, the same length as the title
            Console.WriteLine(new String('-', subTitle.Length));
        }

        private static void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
