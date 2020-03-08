using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VivesRental.Model;

namespace VivesRental.GUI.Converters
{
    class RentalItemStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var status = (RentalItemStatus)value;
                return status.ToString();
            }
            catch (InvalidCastException)
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (RentalItemStatus)Enum.Parse(typeof(RentalItemStatus), (string)value);
            }
            catch (InvalidCastException)
            {
                return value;
            }
        }
    }
}
