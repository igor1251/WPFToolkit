using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для TimePickerControl.xaml
    /// </summary>
    [INotifyPropertyChanged]
    public partial class TimePickerControl : UserControl
    {
        public static readonly DependencyProperty HoursProperty =
            DependencyProperty.Register("Hours", typeof(UInt16), typeof(TimePickerControl));

        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof(UInt16), typeof(TimePickerControl));

        public static readonly DependencyProperty SecondsProperty =
            DependencyProperty.Register("Seconds", typeof(UInt16), typeof(TimePickerControl));

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime), typeof(TimePickerControl));

        const string TIME_VALIDATION_REGEX = @"^([0-2][0-3]:[0-5][0-9]|[0-2][0-3]:[0-5][0-9]:[0-5][0-9])$";

        string timeString = "00:00:00";
        public string TimeString
        {
            get => timeString;
            set
            {
                if (Regex.IsMatch(value, TIME_VALIDATION_REGEX))
                {
                    if (value.Split(':').Length == 2) value += ":00";
                    SetProperty(ref timeString, value);
                }
            }
        }
        
        public TimePickerControl()
        {
            InitializeComponent();
        }
    }
}
