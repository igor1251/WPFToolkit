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
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes;

namespace UserControlsTestArea
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        Func<DataTable> getContent = () =>
        {
            var table = new DataTable();

            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("test1"),
                new DataColumn("test2")
            });

            table.Rows.Add("value1", true);
            table.Rows.Add("value2", false);

            return table;
        };

        [ObservableProperty]
        Func<DataGridColumnDescription[]> getColumns = () =>
        {
            return new DataGridColumnDescription[]
            {
                new DataGridColumnDescription("test1", "Тестовая колонка 1", DataGridColumnType.TEXT_COLUMN),
                new DataGridColumnDescription("test2", "Тестовая колонка 2", DataGridColumnType.CHECKBOX_COLUMN),
            };
        };

        [ObservableProperty]
        DataRowView? selectedRowView;

        [ICommand]
        void ShowSelectedData()
        {
            if (SelectedRowView == null) return;
            MessageBox.Show($"{SelectedRowView.Row["test1"]} {SelectedRowView.Row["test2"]}");
        }
    }
}
