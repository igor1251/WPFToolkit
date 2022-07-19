using CommunityToolkit.Mvvm.ComponentModel;
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
    /// Логика взаимодействия для DateTimePickerControl.xaml
    /// </summary>
    [INotifyPropertyChanged]
    public partial class DateTimePickerControl : UserControl
    {
        public static readonly DependencyProperty SelectedDateTimeProperty =
            DependencyProperty.Register("SelectedDateTime", typeof(DateTime), typeof(DateTimePickerControl));

        public DateTime SelectedDateTime
        {
            get { return (DateTime)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }

        public DateTimePickerControl()
        {
            InitializeComponent();            
        }

        private void ShowCalendarButton_Click(object sender, RoutedEventArgs e)
        {
            CalendarPopup.IsOpen = true;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            CalendarPopup.IsOpen = false;
            if (Calendar.SelectedDate.HasValue)
            {
                var selectedValue = Calendar.SelectedDate.Value;
                selectedValue = selectedValue.AddMinutes(SelectedDateTime.Minute).AddHours(SelectedDateTime.Hour).AddSeconds(SelectedDateTime.Second);
                SelectedDateTime = selectedValue;
            }
        }

        void ProceedDateTimeValidationError(object sender, ValidationErrorEventArgs e)
        {
            DateTimeTextBox.Text = SelectedDateTime.ToString();
        }
    }
}
