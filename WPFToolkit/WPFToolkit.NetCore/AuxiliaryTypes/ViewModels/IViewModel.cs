using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes.TextBoxes;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;

namespace WPFToolkit.NetCore.AuxiliaryTypes.ViewModels
{
    public interface IViewModel
    {
        /// <summary>
        /// Делегат на метод получения содержимого отчета
        /// </summary>
        Func<Task<DataTable>> ReportContentGetter { get; init; }
        /// <summary>
        /// Делегат на метод получения коллекции столбцов, которые 
        /// нужно отобразить в отчете
        /// </summary>
        Func<IEnumerable<DataGridColumnDescription>> DataGridColumnsGetter { get; init; }
        /// <summary>
        /// Делегат на метод получения коллекции кнопок, которые должны отображаться
        /// в отчете
        /// </summary>
        Func<IEnumerable<ButtonDescription>>? ButtonsGetter { get; init; }
        /// <summary>
        /// Делагат на метод получения коллекции пунктов контекстного меню
        /// </summary>
        Func<IEnumerable<MenuItemDescription>>? ContextMenuItemsGetter { get; init; }
        /// <summary>
        /// Делегат на метод получения коллекции пунктов меню окна
        /// </summary>
        Func<IEnumerable<MenuItemDescription>>? WindowMenuItemsGetter { get; init; }
        /// <summary>
        /// Делегат на метод получения полей для ввода
        /// </summary>
        Func<IEnumerable<MarkedTextBoxDescription>>? EntriesGetter { get; init; }
        /// <summary>
        /// Делегат на метод получения описания окна
        /// </summary>
        Func<string> ViewCaptionGetter { get; init; }
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
        MouseButtonEventHandler? RowDoubleClicked { get; init; }
    }
}
