using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFToolkit.NetCore.AuxiliaryTypes.Converters
{
    public class DataRowViewToRowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var row = value as DataRow;
                if (row != null)
                {
                    return row.Table.DefaultView[0];
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var rowView = value as DataRowView;
                if (rowView != null)
                {
                    return rowView.Row;
                }
            }
            return null;
        }
    }
}
