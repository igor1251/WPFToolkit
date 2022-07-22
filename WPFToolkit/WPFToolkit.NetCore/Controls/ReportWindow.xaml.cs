using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using WPFToolkit.NetCore.AuxiliaryTypes;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;
using WPFToolkit.NetCore.AuxiliaryTypes.Universal;
using WPFToolkit.NetCore.AuxiliaryTypes.ViewModels;
using WPFToolkit.NetCore.UIManagers;

namespace WPFToolkit.NetCore.Controls
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    [INotifyPropertyChanged]
    public partial class ReportWindow : Window
    {
        public static readonly DependencyProperty ControlsCollectionProperty =
            DependencyProperty.Register("ControlsCollection", typeof(IDictionary<UIElementLocation, IEnumerable<Guid>>), typeof(ReportWindow));

        public IDictionary<UIElementLocation, IEnumerable<Guid>> ControlsCollection
        {
            get { return (IDictionary<UIElementLocation, IEnumerable<Guid>>)GetValue(ControlsCollectionProperty); }
            set 
            { 
                foreach (var location in value.Keys)
                {
                    foreach (var guid in value[location])
                    {
                        var child = UIElementsManager<Control>.Instance.Get(guid);
                        switch (location)
                        {
                            case UIElementLocation.MENU:
                                WindowMenu.Items.Add(child);
                                break;
                            case UIElementLocation.TOP:
                                TopControlsPanel.Children.Add(child);
                                break;
                            case UIElementLocation.LEFT:
                                LeftControlsPanel.Children.Add(child);
                                break;
                            case UIElementLocation.RIGHT:
                                RightControlsPanel.Children.Add(child);
                                break;
                            case UIElementLocation.BOTTOM:
                                BottomControlsPanel.Children.Add(child);
                                break;
                            case UIElementLocation.CENTER:
                                child.SetValue(Grid.ColumnProperty, 1);
                                child.SetValue(Grid.RowProperty, 2);
                                ContentGrid.Children.Add(child);
                                break;
                        }
                    }
                }
                SetValue(ControlsCollectionProperty, value); 
            }
        }

        public ReportWindow()
        {
            InitializeComponent();
        }
    }
}
