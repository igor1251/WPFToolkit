using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFToolkit.NetCore.AuxiliaryTypes.Menus
{
    public class MenuItemsTemplateCollection : IEnumerable<MenuItemTemplate>
    {
        List<MenuItemTemplate> items = new List<MenuItemTemplate>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header">Заголовок элемента меню</param>
        /// <param name="subItems">Коллекция подэлементов. Может задаваться как MenuItemsTemplateCollection, так и любым другим типом-наследником IEnumerable<MenuItemTemplate></param>
        /// <param name="command">Команда</param>
        public void Add(string header, MenuItemsTemplateCollection? subItems = null, ICommand? command = null, Uri? iconPath = null)
        {
            items.Add(new MenuItemTemplate(header, subItems, command, iconPath));
        }

        public IEnumerator<MenuItemTemplate> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
