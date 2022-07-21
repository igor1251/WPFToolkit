using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes.EntryFields;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;
using WPFToolkit.NetCore.AuxiliaryTypes.ViewModels;
using WPFToolkit.NetCore.Controls;


namespace UserControlsTestArea
{
    public partial class ReportViewModel : ViewModelBase, IViewModel
    {
        public Func<Task<DataTable>> ReportContentGetter { get; init; }
        public Func<IEnumerable<DataGridColumnDescription>> DataGridColumnsGetter { get; init; }
        public Func<IEnumerable<ButtonDescription>>? ButtonsGetter { get; init; }
        public Func<IEnumerable<MenuItemDescription>>? ContextMenuItemsGetter { get; init; }
        public Func<IEnumerable<MenuItemDescription>>? WindowMenuItemsGetter { get; init; }
        public Func<string> ViewCaptionGetter { get; init; }
        public Func<string>? RowFilterGetter { get; set; }
        public DataRow? SelectedRow { get; set; }
        public MouseButtonEventHandler? RowDoubleClicked { get; init; }
        public Func<IEnumerable<EntryDescription>>? EntriesGetter { get; init; }

        static async Task<DataTable> GenDataTable()
        {
            var table = new DataTable();
            Task genTask = new(() =>
            {

                table.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("Key"),
                    new DataColumn("Value")
                });

                bool flag = false;
                for (int i = 1; i <= 100; i++)
                {
                    flag = !flag;
                    table.Rows.Add($"value-{i}", flag);
                }

                Thread.Sleep(2000);
            });
            genTask.Start();
            await genTask;
            return table;
        }

        [ObservableProperty]
        Func<Task<DataTable>> getContent = async () =>
        {
            var table = await GenDataTable();
            return table;
        };

        [ObservableProperty]
        Func<DataGridColumnDescription[]> getColumns = () =>
        {
            return new DataGridColumnDescription[]
            {
                new DataGridColumnDescription("Key", "Ключ", DataGridColumnType.TEXT_COLUMN),
                new DataGridColumnDescription("Value", "Значение", DataGridColumnType.CHECKBOX_COLUMN),
            };
        };

        [ObservableProperty]
        DataRowView? selectedRowView;

        [ObservableProperty]
        string filter = string.Empty;

        [ObservableProperty]
        string entryDescription = "testEntry";

        [ObservableProperty]
        string entryText = "";

        [ObservableProperty]
        DateTime selectedDateTime = DateTime.Now;

        [ICommand]
        void ShowSelectedData()
        {
            if (SelectedRowView == null) return;
            MessageBox.Show($"{SelectedRowView.Row["Key"]} {SelectedRowView.Row["Value"]}");
        }

        [ICommand]
        async void MakeBusy()
        {
            IsBusy = true;
            Task t = new Task(() =>
            {
                Thread.Sleep(5000);
            });
            t.Start();
            await t.ContinueWith((t) =>
            {
                IsBusy = false;
            });
        }

        [ICommand]
        void ApplyFilter()
        {
            Filter = "Key LIKE 'value-30'";
        }

        [ICommand]
        void ShowSelectedDateTime()
        {
            MessageBox.Show(SelectedDateTime.ToString());
        }

        [ICommand]
        void ShowMessage()
        {
            MessageBox.Show("Работает");
        }

        [ICommand]
        void ShowEntryText()
        {
            MessageBox.Show(EntryText);
        }

        public ReportViewModel()
        {
            ReportContentGetter = GetContent;
            DataGridColumnsGetter = GetColumns;
            ViewCaptionGetter = () => $"Заголовок {DateTime.Now.DayOfWeek}";
            RowFilterGetter = () => "Key LIKE 'value-30'";
            RowDoubleClicked = (sender, e) => { MessageBox.Show("Да, работает"); };
            WindowMenuItemsGetter = () =>
            {
                return new List<MenuItemDescription>
                {
                    new MenuItemDescription("File", new List<MenuItemDescription>()
                    {
                        new MenuItemDescription("Open", null, ShowMessageCommand),
                    }, null),
                };
            };
            ContextMenuItemsGetter = () =>
            {
                return new List<MenuItemDescription>
                {
                    new MenuItemDescription("Yeah", new List<MenuItemDescription>()
                    {
                        new MenuItemDescription("UwU", null, ShowMessageCommand),
                    }, null),
                };
            };
            ButtonsGetter = () =>
            {
                return new List<ButtonDescription>()
                {
                    new ButtonDescription("Show Entry Text", ShowEntryTextCommand, ButtonType.NONE, WPFToolkit.NetCore.AuxiliaryTypes.Universal.UIElementLocation.BOTTOM),
                    new ButtonDescription("Test", ShowMessageCommand, ButtonType.NONE, WPFToolkit.NetCore.AuxiliaryTypes.Universal.UIElementLocation.LEFT),
                    new ButtonDescription("Test", ShowMessageCommand, ButtonType.NONE, WPFToolkit.NetCore.AuxiliaryTypes.Universal.UIElementLocation.RIGHT),
                    new ButtonDescription("Test", ShowMessageCommand, ButtonType.NONE, WPFToolkit.NetCore.AuxiliaryTypes.Universal.UIElementLocation.TOP)
                };
            };
            EntriesGetter = () =>
            {
                return new List<EntryDescription>()
                {
                    new EntryDescription("shit", WPFToolkit.NetCore.AuxiliaryTypes.Universal.UIElementLocation.BOTTOM, (sender, e) =>
                    {
                        EntryText = ((TextBox)sender).Text;
                    })
                };
            };
        }
    }
}
