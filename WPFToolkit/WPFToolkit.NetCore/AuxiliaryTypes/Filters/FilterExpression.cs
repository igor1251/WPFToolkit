using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToolkit.NetCore.AuxiliaryTypes.Filters
{
    public class FilterExpression
    {
        public string ColumnName { get; set; } = string.Empty;
        public Type RequiredValueType { get; set; } = typeof(object);
        public object RequiredValue { get; set; } = new();

        public FilterExpression(string columnName, Type requiredValueType, object requiredValue)
        {
            ColumnName = columnName;
            RequiredValue = requiredValue;
            RequiredValueType = requiredValueType;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(ColumnName) || RequiredValueType == null || RequiredValue == null) return string.Empty;
            if (RequiredValueType == typeof(string)) return $"{ColumnName} LIKE '%{RequiredValue}%'";
            else if (RequiredValueType == typeof(Double) ||
                     RequiredValueType == typeof(Single)) return $"{ColumnName} = {RequiredValue?.ToString()?.Replace(',', '.')}";
            else if (RequiredValueType == typeof(UInt16) ||
                     RequiredValueType == typeof(UInt32) ||
                     RequiredValueType == typeof(UInt64) ||
                     RequiredValueType == typeof(Int16) ||
                     RequiredValueType == typeof(Int32) ||
                     RequiredValueType == typeof(Int64) ||
                     RequiredValueType == typeof(Boolean)) return $"{ColumnName} = {RequiredValue}";
            else if (RequiredValueType == typeof(DateTime)) return $"{ColumnName} = #{Convert.ToDateTime(RequiredValue):yyyy/MM/dd HH:mm:ss}#)";
            else return string.Empty;
        }
    }
}
