using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToolkit.NetCore.AuxiliaryTypes.Filters
{
    public class FilterExpressionsCollection : IEnumerable<FilterExpression>
    {
        List<FilterExpression> expressions = new();

        public void Add(string columnName, Type requiredValueType, object requiredValue)
        {
            expressions.Add(new FilterExpression(columnName, requiredValueType, requiredValue));
        }

        public IEnumerator<FilterExpression> GetEnumerator()
        {
            return expressions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(" AND ", expressions);
        }
    }
}
