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
    /// ��������� ��������� ����������, ��� ����������� ��������� ������ � 
    /// ����������� ������������ �����, ����������� ������ ����� ��������
    /// (��� ���������� �������������)
    /// </summary>
    public partial class BusyControl : UserControl
    {
        public static DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyControl), new PropertyMetadata(false));

        public static DependencyProperty BusyContentProperty = 
            DependencyProperty.Register("BusyContent", typeof(string), typeof(BusyControl), new PropertyMetadata("���������...."));

        public static DependencyProperty BackgroundColorProperty = 
            DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(BusyControl), new PropertyMetadata(Color.FromArgb(192, 105, 105, 105)));

        public static DependencyProperty BackgroundOpacityProperty =
            DependencyProperty.Register("BackgroundOpacity", typeof(double), typeof(BusyControl), new PropertyMetadata(1.0));

        /// <summary>
        /// ��������� �������� ����������
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// ��������� ����������, ������������ ��� ��������� �������� ����������
        /// </summary>
        public string BusyContent
        {
            get { return (string)GetValue(BusyContentProperty); }
            set { SetValue(BusyContentProperty, value); }
        }

        /// <summary>
        /// ���� ����, ������������� ��� ��������� ����������.
        /// ������������ ����� ������������ ��� ������ ��������� 'a' ���
        /// ������� Color.FromArgb
        /// </summary>
        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        /// <summary>
        /// ������������ ���� �������� ���������� (����� ����� ��������������
        /// ������������ ��������� 'a' ��� ������� Color.FromArgb ��� ��������
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