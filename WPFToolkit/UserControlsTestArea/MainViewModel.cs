using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.Controls;
using WPFToolkit.NetCore.AuxiliaryTypes.ViewModels;
using WPFToolkit.NetCore.UIManagers;
using System.Windows.Controls;
using System.Collections.Generic;
using WPFToolkit.NetCore.AuxiliaryTypes.Universal;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.TextBoxes;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;

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

        [ICommand]
        void RunReportWindow()
        {
            var win = new ReportWindow(new ReportViewModel());
            win.ShowDialog();
        }
    }
}
