using System;
using System.Globalization;
using System.Windows.Data;

namespace TidyPDF.View.Converter
{
    public class QualityToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(Quality))
                throw new ArgumentException("Invalid type. Must be of type 'Quality'");

            return value switch
            {
                Quality.Excellent => "0",
                Quality.Good => "1",
                Quality.Average => "2",
                Quality.Bad => "3",
                _ => "?"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}