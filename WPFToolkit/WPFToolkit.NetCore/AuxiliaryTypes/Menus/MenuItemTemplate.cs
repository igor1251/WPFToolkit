using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WPFToolkit.NetCore.AuxiliaryTypes.Menus
{
    public class MenuItemTemplate
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
        public IEnumerable<MenuItemTemplate>? SubItems { get; set; }
        /// <summary>
        /// Команда, исполняемая при нажатии на пункт меню
        /// </summary>
        public ICommand? Command = null;
        /// <summary>
        /// Путь до иконки в формате Uri
        /// </summary>
        public Uri? IconPath = null;
        public MenuItemTemplate(string header, IEnumerable<MenuItemTemplate>? subItems, ICommand? command, Uri? iconPath = null)
        {
            Header = header;
            SubItems = subItems;
            Command = command;
            IconPath = iconPath;
        }
    }
}
