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
    /// Логика взаимодействия для MarkedTextBoxControl.xaml
    /// </summary>
    public partial class MarkedTextBoxControl : UserControl
    {
        public static readonly DependencyProperty LabelProperty = 
            DependencyProperty.Register("Label", typeof(string), typeof(MarkedTextBoxControl), new PropertyMetadata("LabelText"));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MarkedTextBoxControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextChangedProperty =
            DependencyProperty.Register("TextChanged", typeof(TextChangedEventHandler), typeof(MarkedTextBoxControl));

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
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
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
