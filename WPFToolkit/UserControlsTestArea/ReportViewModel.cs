using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;
using WPFToolkit.NetCore.AuxiliaryTypes.ViewModels;

namespace UserControlsTestArea
{
    public class ReportViewModel : IViewModel
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
    }
}
