using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WPFToolkit.NetCore.AuxiliaryTypes.Menus
{
    public class MenuItemDescription
    {
        string header = "menuitem";
        
        /// <summary>
        /// Заголовок элемента контекстного меню
        /// </summary>
        public string Header
        {
            get => header;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("Заголовок элемента меню не может быть пустым");
                header = value;
            }
        }
        /// <summary>
        /// Дочерние элементы
        /// </summary>
        public IEnumerable<MenuItemDescription>? SubItems { get; set; }
        /// <summary>
        /// Команда, исполняемая при нажатии на пункт меню
        /// </summary>
        public ICommand? Command = null;
        public MenuItemDescription(string header, IEnumerable<MenuItemDescription>? subItems, ICommand? command)
        {
            Header = header;
            SubItems = subItems;
            Command = command;
        }
    }
}
