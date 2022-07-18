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
        public static readonly DependencyProperty DescriptionProperty = 
            DependencyProperty.Register("Description", typeof(string), typeof(MarkedTextBoxControl), new PropertyMetadata("LabelText"));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MarkedTextBoxControl), new PropertyMetadata(string.Empty));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public MarkedTextBoxControl()
        {
            InitializeComponent();
        }
    }
}
