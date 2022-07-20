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
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.Controls;

namespace UserControlsTestArea
{
    public partial class MainViewModel : ViewModelBase
    {
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
        string entryText = string.Empty;

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
        void RunReportWindow()
        {
            var viewModel = new ReportViewModel()
            {
                ReportContentGetter = GetContent,
                DataGridColumnsGetter = GetColumns,
                ViewCaptionGetter = () => $"Заголовок {DateTime.Now.DayOfWeek}",
                RowFilterGetter = () => Filter,
                RowDoubleClicked = (sender, e) => { MessageBox.Show("Да, работает"); }
            };
            var win = new ReportWindow(viewModel);
            win.ShowDialog();
        }
    }
}
