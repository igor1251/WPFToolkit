using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;

namespace WPFToolkit.NetCore.AuxiliaryTypes.ViewModels
{
    public interface IViewModel
    {
        /// <summary>
        /// Делегат на метод получения содержимого отчета
        /// </summary>
        Func<Task<DataTable>>? ReportContentGetter { get; set; }
        /// <summary>
        /// Делегат на метод получения коллекции столбцов, которые 
        /// нужно отобразить в отчете
        /// </summary>
        Func<DataGridColumnDescription[]>? DataGridColumnsGetter { get; set; }
        /// <summary>
        /// Делегат на метод получения коллекции кнопок, которые должны отображаться
        /// в отчете
        /// </summary>
        Func<ButtonDescription[]>? ButtonsGetter { get; set; }
        /// <summary>
        /// Делегат на метод получения описания окна
        /// </summary>
        Func<string>? ViewCaptionGetter { get; set; }
        /// <summary>
        /// Делегат на метод получения фильтра для отображаемых данных
        /// </summary>
        Func<string>? RowFilterGetter { get; set; }
        /// <summary>
        /// Выбранная строка
        /// </summary>
        DataRow? SelectedRow { get; set; }
        /// <summary>
        /// Обработчик события двойного клика по DataGrid
        /// </summary>
        MouseButtonEventHandler? RowDoubleClicked { get; set; }
    }
}
