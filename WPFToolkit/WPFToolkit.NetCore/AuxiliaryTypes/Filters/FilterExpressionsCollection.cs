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
        readonly List<FilterExpression> expressions = new();
        /// <summary>
        /// Добавляет новый фильтр в коллекцию фильтров
        /// </summary>
        /// <param name="columnName">Имя столбца, по которому будет работать фильтр</param>
        /// <param name="requiredValueType">Тип значения, по которому будет выполняться фильтрация</param>
        /// <param name="requiredValue">Значение фильтра</param>
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
            return string.Join(" AND ", expressions.Where(filter => !string.IsNullOrEmpty(filter.ToString())));
        }
    }
}
