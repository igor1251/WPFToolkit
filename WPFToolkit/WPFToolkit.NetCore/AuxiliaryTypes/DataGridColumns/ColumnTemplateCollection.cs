using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns
{
    public class ColumnTemplateCollection : IEnumerable<ColumnTemplate>
    {
        List<ColumnTemplate> columns = new List<ColumnTemplate>();

        public void Add(string inDatabaseName, string DisplayName, ColumnType type)
        {
            columns.Add(new ColumnTemplate(inDatabaseName, DisplayName, type));
        }

        public IEnumerator<ColumnTemplate> GetEnumerator()
        {
            return columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
