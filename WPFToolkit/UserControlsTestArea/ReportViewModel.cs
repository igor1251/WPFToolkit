using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes.TextBoxes;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;
using WPFToolkit.NetCore.AuxiliaryTypes.ViewModels;
using WPFToolkit.NetCore.AuxiliaryTypes.Universal;
using System.Windows.Media;
using WPFToolkit.NetCore.UIManagers;
using System.Text.RegularExpressions;

namespace UserControlsTestArea
{
    public partial class ReportViewModel : ViewModelBase
    {
        public Dictionary<UIElementLocation, IEnumerable<Guid>> Controls = new();

        string userText = string.Empty;
        public string UserText
        {
            get => userText;
            set
            {
                if (Regex.IsMatch(value, "^([0-9]|[1-9][0-9])$"))
                {
                    SetProperty(ref userText, value);
                }
            }
        }

        [ICommand]
        void ShowText()
        {
            MessageBox.Show(UserText);
        }

        public ReportViewModel()
        {
            Controls.Add(UIElementLocation.TOP, new List<Guid>()
            {
                UIElementsDecorator.CreateMarkedTextBox(new MarkedTextBoxDescription("test", new System.Windows.Data.Binding("UserText")
                {
                    UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged,
                })),
            });
            Controls.Add(UIElementLocation.BOTTOM, new List<Guid>()
            {
                UIElementsDecorator.CreateButton(new ButtonDescription("test", ShowTextCommand)),
            });
        }
    }
}
