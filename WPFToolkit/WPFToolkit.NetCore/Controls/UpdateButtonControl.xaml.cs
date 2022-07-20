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
    /// Логика взаимодействия для UpdateButtonControl.xaml
    /// </summary>
    public partial class UpdateButtonControl : UserControl
    {
        public static readonly DependencyProperty UpdateCommandProperty =
            DependencyProperty.Register("UpdateCommand", typeof(ICommand), typeof(UpdateButtonControl));

        /// <summary>
        /// Команда, выполняемая пр нажатии на элемент управления
        /// </summary>
        public ICommand UpdateCommand
        {
            get { return (ICommand)GetValue(UpdateCommandProperty); }
            set { SetValue(UpdateCommandProperty, value); }
        }

        public UpdateButtonControl()
        {
            InitializeComponent();
        }
    }
}
