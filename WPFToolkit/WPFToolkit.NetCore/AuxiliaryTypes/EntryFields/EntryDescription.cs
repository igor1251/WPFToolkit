using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPFToolkit.NetCore.AuxiliaryTypes.Universal;

namespace WPFToolkit.NetCore.AuxiliaryTypes.EntryFields
{
    public class EntryDescription : BaseUIElementDescription
    {
        string label = "entry";
        /// <summary>
        /// Описание поля для ввода, отображается слева
        /// </summary>
        public string Label
        {
            get => label;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Описание поля для ввода не может быть пустым");
                label = value;
            }
        }
        //не работает привязка, заменена костылем - обработчиком события изменения текста
        public TextChangedEventHandler? TextChanged { get; init; }
        /// <summary>
        /// Создает описание маркированного поля для ввода
        /// </summary>
        /// <param name="label">Описание</param>
        /// <param name="location">Расположение</param>
        /// <param name="textChanged">Обработчик события изменения текста</param>
        public EntryDescription(string label, UIElementLocation location, TextChangedEventHandler? textChanged)
        {
            Label = label;
            Location = location;
            TextChanged = textChanged;
        }
    }
}
