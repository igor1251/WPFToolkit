using System;
using System.Windows.Controls;
using WPFToolkit.NetCore.AuxiliaryTypes.Universal;

namespace WPFToolkit.NetCore.AuxiliaryTypes.TextBoxes
{
    public class MarkedTextBoxDescription : BaseUIElementDescription
    {
        string label = "MarkedTextBox";
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
        /// <summary>
        /// Регулярное выражение для валидации пользовательского ввода
        /// </summary>
        public string RegexMast { get; set; } = string.Empty;
        //не работает привязка, заменена костылем - обработчиком события изменения текста
        public TextChangedEventHandler? TextChanged { get; init; }
        /// <summary>
        /// Создает описание маркированного поля для ввода
        /// </summary>
        /// <param name="label">Описание</param>
        /// <param name="location">Расположение</param>
        /// <param name="textChanged">Обработчик события изменения текста</param>
        /// <param name="regexMask">Регулярное выражение, фильтрующее ввод</param>
        public MarkedTextBoxDescription(string label, UIElementLocation location, TextChangedEventHandler? textChanged = null, string regexMask = "")
        {
            Label = label;
            Location = location;
            TextChanged = textChanged;
            RegexMast = regexMask;
        }
    }
}
