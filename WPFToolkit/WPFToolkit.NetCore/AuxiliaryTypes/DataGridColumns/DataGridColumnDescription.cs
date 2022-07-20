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
    public class DataGridColumnDescription
    {
        /// <summary>
        /// Тип столбца
        /// </summary>
        public DataGridColumnType ColumnType { get; set; }
        /// <summary>
        /// Имя столбца в БД
        /// </summary>
        public string InDatabaseName { get; set; }
        /// <summary>
        /// Отображаемое имя стобца во отчете
        /// </summary>
        public string DisplayName { get; set; }

        public DataGridColumnDescription()
        {
            ColumnType = DataGridColumnType.TEXT_COLUMN;
            InDatabaseName = string.Empty;
            DisplayName = string.Empty;
        }

        public DataGridColumnDescription(string inDatabaseName, string displayName, DataGridColumnType columnType = DataGridColumnType.TEXT_COLUMN)
        {
            ColumnType = columnType;
            InDatabaseName = inDatabaseName;
            DisplayName = displayName;
        }
    }
}
