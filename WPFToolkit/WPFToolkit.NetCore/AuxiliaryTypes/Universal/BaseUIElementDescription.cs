using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFToolkit.NetCore.AuxiliaryTypes.Universal
{
    public class BaseUIElementDescription
    {
        /// <summary>
        /// DataContext для элемента управления (необходим для привязки)
        /// </summary>
        public Binding Binding { get; set; } = new();
    }
}
