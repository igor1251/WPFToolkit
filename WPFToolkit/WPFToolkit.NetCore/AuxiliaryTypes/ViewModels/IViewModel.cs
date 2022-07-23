using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes.TextBoxes;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;
using WPFToolkit.NetCore.AuxiliaryTypes.Universal;

namespace WPFToolkit.NetCore.AuxiliaryTypes.ViewModels
{
    public interface IViewModel
    {
        /// <summary>
        /// Делегат на метод получения описания окна
        /// </summary>
        Func<string> ViewCaptionGetter { get; init; }
        /// <summary>
        /// Словарь элементов упарвления, доступных к отображению на форме
        /// </summary>
        Dictionary<UIElementLocation, IEnumerable<Guid>> Controls { get; init; }
    }
}
