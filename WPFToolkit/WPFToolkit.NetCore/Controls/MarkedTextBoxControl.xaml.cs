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
    /// Логика взаимодействия для MarkedTextBoxControl.xaml
    /// </summary>
    [INotifyPropertyChanged]
    public partial class MarkedTextBoxControl : UserControl
    {
        public static readonly DependencyProperty LabelProperty = 
            DependencyProperty.Register("Label", typeof(string), typeof(MarkedTextBoxControl), new PropertyMetadata("LabelText"));

        public static readonly DependencyProperty TextChangedProperty =
            DependencyProperty.Register("TextChanged", typeof(TextChangedEventHandler), typeof(MarkedTextBoxControl));

        public static readonly DependencyProperty RegexMaskProperty =
            DependencyProperty.Register("RegexMask", typeof(string), typeof(MarkedTextBoxControl));

        /// <summary>
        /// Регулярное выражение, которое будет управлять вводом в элемент управления
        /// </summary>
        public string RegexMask
        {
            get { return (string)GetValue(RegexMaskProperty); }
            set { SetValue(RegexMaskProperty, value); }
        }
        /// <summary>
        /// Обработчик события изменения текста в элементе управления
        /// </summary>
        public TextChangedEventHandler TextChanged
        {
            get { return (TextChangedEventHandler)GetValue(TextChangedProperty); }
            set { SetValue(TextChangedProperty, value); }
        }
        /// <summary>
        /// Описание поля для ввода, отображается слева
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        /// <summary>
        /// Введенный текст
        /// </summary>
        string text = string.Empty;
        public string Text
        {
            get => text;
            set 
            {
                if (Regex.IsMatch(value, RegexMask) || string.IsNullOrEmpty(value))
                {
                    SetProperty(ref text, value);
                }
            }
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MarkedTextBoxControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработчик события готовности элемента управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkedTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (TextChanged != null)
                TextBox.TextChanged += TextChanged;
        }
    }
}
