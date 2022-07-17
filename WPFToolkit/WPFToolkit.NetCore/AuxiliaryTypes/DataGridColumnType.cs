using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToolkit.NetCore.AuxiliaryTypes
{
    /// <summary>
    /// Тип колонки в DataGrid
    /// </summary>
    public enum DataGridColumnType
    {
        /// <summary>
        /// Простой столбец с тектовыми данными
        /// </summary>
        TEXT_COLUMN,
        /// <summary>
        /// Столбец в виде CheckBox для представления 
        /// данных с типом Boolean
        /// </summary>
        CHECKBOX_COLUMN
    }
}
