using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes.ViewModels;

namespace UserControlsTestArea
{
    public class ReportViewModel : IViewModel
    {
        public Func<Task<DataTable>>? ReportContentGetter { get; set; }
        public Func<DataGridColumnDescription[]>? DataGridColumnsGetter { get; set; }
        public Func<ButtonDescription[]>? ButtonsGetter { get; set; }
        public Func<string>? ViewCaptionGetter { get; set; }
        public Func<string>? RowFilterGetter { get; set; }
        public DataRow? SelectedRow { get; set; }
        public MouseButtonEventHandler? RowDoubleClicked { get; set; }
    }
}
