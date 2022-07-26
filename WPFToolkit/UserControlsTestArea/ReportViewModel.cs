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
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes;
using System.Windows.Controls;
using System.Data;
using WPFToolkit.NetCore.AuxiliaryTypes.Filters;

namespace UserControlsTestArea
{
    [INotifyPropertyChanged]
    public partial class ReportViewModel : IViewModel
    {
        [ObservableProperty]
        bool isBusy = false;

        [ObservableProperty]
        string title = string.Empty;

        [ObservableProperty]
        string rowFilter = string.Empty;

        [ObservableProperty]
        DataTable table = new DataTable();

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

        [ObservableProperty]
        DataRow? selectedRow;

        [ObservableProperty]
        bool isTextBoxEnabled = true;

        static async Task<DataTable> GenDataTable()
        {
            var table = new DataTable();
            Task genTask = new(() =>
            {

                table.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("col1"),
                    new DataColumn("col2")
                });

                bool flag = false;
                for (int i = 1; i <= 100; i++)
                {
                    flag = !flag;
                    table.Rows.Add(flag, $"value-{i}");
                }
                Thread.Sleep(2000);
            });
            genTask.Start();
            await genTask;
            return table;
        }

        public Func<string> ViewCaptionGetter 
        { 
            get => () => $"Caption example {DateTime.Now}";
        }

        public Dictionary<UIElementLocation, IEnumerable<Guid>> Controls { get; } = new();

        [ICommand]
        async void Update()
        {
            Title = ViewCaptionGetter.Invoke();
            IsBusy = true;
            Table = await GenDataTable();
            IsBusy = false;
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

        [ICommand]
        void ApplyFilter()
        {
            FilterExpressionsCollection filters = new()
            {
                { "col1", typeof(Boolean), true },
                { "col2", typeof(string), UserText }
            };
            Table.DefaultView.RowFilter = filters.ToString();
        }

        [ICommand]
        void ShowSelectedRow()
        {
            if (SelectedRow != null)
                MessageBox.Show($"col1 = {SelectedRow["col1"]}\ncol2 = {SelectedRow["col2"]}");
        }

        [ICommand]
        void Switch()
        {
            IsTextBoxEnabled = !IsTextBoxEnabled;
        }

        public ReportViewModel()
        {
            Controls.Add(UIElementLocation.TOP, new List<Guid>()
            {
                UIElementsDecorator.CreateMarkedTextBox("test", nameof(UserText), nameof(IsTextBoxEnabled)),
            });
            Controls.Add(UIElementLocation.BOTTOM, new List<Guid>()
            {
                UIElementsDecorator.CreateButton("Show user input", ShowTextCommand, null, nameof(IsTextBoxEnabled)),
                UIElementsDecorator.CreateButton("Update", UpdateCommand, Color.FromRgb(250, 105, 105)),
                UIElementsDecorator.CreateButton("Make busy", MakeBusyCommand),
                UIElementsDecorator.CreateButton("Apply filter", ApplyFilterCommand, (Color)ColorConverter.ConvertFromString("#0099ff")),
                UIElementsDecorator.CreateButton("Show selected row", ShowSelectedRowCommand),
                UIElementsDecorator.CreateButton("Switch textBox Enabled", SwitchCommand),
            });
            Controls.Add(UIElementLocation.MENU, new List<Guid>()
            {
                UIElementsDecorator.CreateMenu(new MenuItemsTemplateCollection()
                {
                    { "Print", new MenuItemsTemplateCollection()
                    {
                        { "Show greeting", null, ShowGreetingCommand, new Uri("pack://application:,,,/UserControlsTestArea;component/Assets/Icons/greet.png") }
                    }
                    }
                }, "File"),
            });
            Controls.Add(UIElementLocation.CENTER, new List<Guid>()
            {
                UIElementsDecorator.CreateDataGrid(new ColumnTemplateCollection
                {
                    { "col1", "First column", ColumnType.CHECKBOX_COLUMN },
                    { "col2", "SecondColumn", ColumnType.TEXT_COLUMN },
                }, "Table", nameof(SelectedRow), UIElementsDecorator.CreateMenu(new MenuItemsTemplateCollection()
                {
                    { "DataGrid conntext menu test", new MenuItemsTemplateCollection()
                    {
                        { "Run test command", null, ShowGreetingCommand }
                    }, null }
                })),
            });
            Update();            
        }
    }
}