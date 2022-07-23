using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.TextBoxes;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;
using WPFToolkit.NetCore.AuxiliaryTypes.ViewModels;
using WPFToolkit.NetCore.AuxiliaryTypes.Universal;
using WPFToolkit.NetCore.UIManagers;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace UserControlsTestArea
{
    [INotifyPropertyChanged]
    public partial class ReportViewModel : IViewModel
    {
        [ObservableProperty]
        bool isBusy = false;

        [ObservableProperty]
        string title = string.Empty;

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

        public Func<string> ViewCaptionGetter 
        { 
            get => () => $"Caption example {DateTime.Now}";
        }

        public Dictionary<UIElementLocation, IEnumerable<Guid>> Controls { get; } = new();

        [ICommand]
        void Update()
        {
            Title = ViewCaptionGetter.Invoke();
        }

        [ICommand]
        void ShowText()
        {
            MessageBox.Show(UserText);
        }

        [ICommand]
        async void MakeBusy()
        {
            IsBusy = true;
            await Task.Run(() => Thread.Sleep(3000));
            IsBusy = false;
        }

        [ICommand]
        void ShowGreeting()
        {
            MessageBox.Show("!!!Greetings!!!");
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
                UIElementsDecorator.CreateButton(new ButtonDescription("Show user input", ShowTextCommand)),
                UIElementsDecorator.CreateButton(new ButtonDescription("Update", UpdateCommand, Color.FromRgb(250, 105, 105))),
                UIElementsDecorator.CreateButton(new ButtonDescription("Make busy", MakeBusyCommand)),
            });
            Controls.Add(UIElementLocation.MENU, new List<Guid>()
            {
                UIElementsDecorator.CreateMenu(new List<MenuItemDescription>()
                {
                    new MenuItemDescription("Print", new List<MenuItemDescription>()
                    {
                        new MenuItemDescription("Greeting", null, ShowGreetingCommand),
                    }, null)
                }, "File"),
            });
            Update();
        }
    }
}