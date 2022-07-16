using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFToolkit.NetCore.Controls
{
    /// <summary>
    /// Элемент управления, позволяющий настраивать временной промежуток
    /// </summary>
    public partial class PeriodControl : UserControl
    {
        public static DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(DateTime), typeof(PeriodControl), new PropertyMetadata(DateTime.Now));

        public static DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(DateTime), typeof(PeriodControl), new PropertyMetadata(DateTime.Now));

        public static DependencyProperty SelectedDateFormatProperty =
            DependencyProperty.Register("SelectedDateFormat", typeof(DatePickerFormat), typeof(PeriodControl), new PropertyMetadata(DatePickerFormat.Short));

        /// <summary>
        /// Начало временного интервала в формате дата-время
        /// </summary>
        public DateTime From
        {
            get { return (DateTime)GetValue(FromProperty); }
            set 
            {
                if (value <= To)
                {
                    SetValue(FromProperty, value);
                }
            }
        }

        /// <summary>
        /// Конец временного интервала в формате дата-время
        /// </summary>
        public DateTime To
        {
            get { return (DateTime)GetValue(ToProperty); }
            set 
            { 
                if (value >= From)
                {
                    SetValue(ToProperty, value);
                }
            }
        }

        /// <summary>
        /// Формат отображаемого времени Long - со временем, Short - только дата
        /// </summary>
        public DatePickerFormat SelectedDateFormat
        {
            get { return (DatePickerFormat)GetValue(SelectedDateFormatProperty); }
            set { SetValue(SelectedDateFormatProperty, value); }
        }

        public PeriodControl()
        {
            InitializeComponent();
        }

        private void moveLeftButton_Click(object sender, RoutedEventArgs e)
        {
            From = From.AddDays(-1);
            To = To.AddDays(-1);
        }

        private void moveRightButton_Click(object sender, RoutedEventArgs e)
        {
            To = To.AddDays(1);
            From = From.AddDays(1);
        }
    }
}
