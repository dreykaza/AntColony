using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace AntColony.Visualization
{
    public class CellTypeToColorConverter : IValueConverter
    {
        public static readonly CellTypeToColorConverter Instance = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value switch
            {
                0 => Brushes.White,
                1 => Brushes.Black,
                2 => Brushes.Yellow,
                3 => Brushes.Red,
                4 => Brushes.Purple,
                _ => Brushes.White,
            };
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            throw new NotImplementedException();
        }
    }
}
