using System;
using System.Windows.Input;

namespace WPFToolkit.NetCore.AuxiliaryTypes.ContextMenus
{
    public class ContextMenuItemDescription
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
        /// Команда, исполняемая при нажатии на пункт меню
        /// </summary>
        public ICommand? Command = null;
    }
}
