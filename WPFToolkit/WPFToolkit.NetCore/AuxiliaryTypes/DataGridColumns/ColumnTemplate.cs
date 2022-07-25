using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns
{
    /// <summary>
    /// Класс для описания элемента коллекции столбцов 
    /// для DataGrid
    /// </summary>
    public class ColumnTemplate
    {
        /// <summary>
        /// Тип столбца
        /// </summary>
        public ColumnType ColumnType { get; set; }
        /// <summary>
        /// Имя столбца в БД
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// Отображаемое имя стобца во отчете
        /// </summary>
        public string Header { get; set; }

        public ColumnTemplate()
        {
            ColumnType = ColumnType.TEXT_COLUMN;
            ColumnName = string.Empty;
            Header = string.Empty;
        }

        public ColumnTemplate(string inDatabaseName, string displayName, ColumnType columnType = ColumnType.TEXT_COLUMN)
        {
            ColumnType = columnType;
            ColumnName = inDatabaseName;
            Header = displayName;
        }
    }
}
