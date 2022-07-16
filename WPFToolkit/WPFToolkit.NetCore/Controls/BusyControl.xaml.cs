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
    /// »ндикатор зан€тости приложени€, при отображении блокирует доступ к 
    /// содержимому родительской формы, отобража€сь поверх всего контента
    /// (при правильном использовании)
    /// </summary>
    public partial class BusyControl : UserControl
    {
        public static DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyControl), new PropertyMetadata(false));

        public static DependencyProperty BusyContentProperty = 
            DependencyProperty.Register("BusyContent", typeof(string), typeof(BusyControl), new PropertyMetadata("ѕодождите...."));

        public static DependencyProperty BackgroundColorProperty = 
            DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(BusyControl), new PropertyMetadata(Color.FromArgb(192, 105, 105, 105)));

        public static DependencyProperty BackgroundOpacityProperty =
            DependencyProperty.Register("BackgroundOpacity", typeof(double), typeof(BusyControl), new PropertyMetadata(1.0));

        /// <summary>
        /// јктиватор элемента упарвлени€
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// “екстовое содержимое, отображаемое при активации элемента управлени€
        /// </summary>
        public string BusyContent
        {
            get { return (string)GetValue(BusyContentProperty); }
            set { SetValue(BusyContentProperty, value); }
        }

        /// <summary>
        /// ÷вет фона, отображаемого под элементом управлени€.
        /// ѕрозрачность можно регулировать при помощи параметра 'a' при
        /// задании Color.FromArgb
        /// </summary>
        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        /// <summary>
        /// ѕрозрачность фона элемента управлени€ (также может регулироватьс€
        /// выставлением параметра 'a' при задании Color.FromArgb дл€ свойства
        /// BackgroundColor
        /// </summary>
        public double BackgroundOpacity
        {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }

        public BusyControl()
        {
            InitializeComponent();
        }
    }
}