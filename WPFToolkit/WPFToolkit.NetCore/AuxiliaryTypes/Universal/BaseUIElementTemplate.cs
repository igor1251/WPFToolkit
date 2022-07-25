using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFToolkit.NetCore.AuxiliaryTypes.Universal
{
    public class BaseUIElementTemplate
    {
        /// <summary>
        /// Доступен ли элемент управления для взаимодействия
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }
}
