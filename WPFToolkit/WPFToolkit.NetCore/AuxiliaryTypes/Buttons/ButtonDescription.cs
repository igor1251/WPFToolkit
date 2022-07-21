using System;
using System.Windows.Input;
using System.Windows.Media;
using WPFToolkit.NetCore.AuxiliaryTypes.Universal;

namespace WPFToolkit.NetCore.AuxiliaryTypes.Buttons
{
    public class ButtonDescription
    {
        string content = "button";
        /// <summary>
        /// Заголовок кнопки
        /// </summary>
        public string Content
        {
            get => content;
            set
            {
                if (string.IsNullOrEmpty(content)) throw new ArgumentException("Текст кнопки не может быть пустым");
                content = value;
            }
        }
        /// <summary>
        /// Команда, выполняемая при нажатии на кнопку
        /// </summary>
        public ICommand? Command = null;
        /// <summary>
        /// Цвет кнопки
        /// </summary>
        public Color? BackgroundColor { get; set; }
        /// <summary>
        /// Тип кнопки (ссылка, обычная или большая)
        /// </summary>
        public ButtonType Type { get; set; } = ButtonType.NONE;
        /// <summary>
        /// В каком контейнере расположить кнопку
        /// </summary>
        public UIElementLocation Location { get; set; } = UIElementLocation.BOTTOM;
        /// <summary>
        /// Конструктор лкасса, описывающего кнопку
        /// </summary>
        /// <param name="content">Название кнопки</param>
        /// <param name="command">Команда, выполняемая при нажатии на кнопку</param>
        /// <param name="type">Тип кнопки (ссылка, обычная или большая)</param>
        /// <param name="location">В каком контейнере расположить кнопку</param>
        /// <param name="color">Цвет кнопки</param>
        public ButtonDescription(string content, ICommand? command, ButtonType type = ButtonType.BUTTON, UIElementLocation location = UIElementLocation.BOTTOM, Color? color = null)
        {
            Content = content;
            Command = command;
            Type = type;
            Location = location;
            BackgroundColor = color;
        }
    }
}
