using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFToolkit.NetCore.UIManagers
{
    public class UIElementsManager<T> where T : Control, new()
    {
        static UIElementsManager<T> instance;
        UIElementsManager() { }
        /// <summary>
        /// Статический экземпляр класса
        /// </summary>
        public static UIElementsManager<T> Instance
        {
            get => instance ?? (instance = new UIElementsManager<T>());
        }
        Dictionary<Guid, T> UIElementsCollection = new();
        /// <summary>
        /// Позволяет добавить Control в коллекцию элементов приложения
        /// </summary>
        /// <param name="element">Control, который нужно добавить</param>
        /// <returns>Guid, присвоенный элементу, по которому его можно будет получить</returns>
        public Guid Add(T element)
        {
            var elementGuid = Guid.NewGuid();
            UIElementsCollection.Add(elementGuid, element);
            return elementGuid;
        }
        /// <summary>
        /// Позволяет получить Control по его Guid
        /// </summary>
        /// <param name="guid">Guid, присвоенный элементу при добавлении в коллекцию</param>
        /// <returns></returns>
        public T Get(Guid guid)
        {
            return UIElementsCollection[guid];
        }
        /// <summary>
        /// Получает Guid элемента
        /// </summary>
        /// <param name="element">Элемент, Guid которого мы хотим получить</param>
        /// <returns></returns>
        public Guid Get(T element)
        {
            return UIElementsCollection.First(item => item.Value == element).Key;
        }
        /// <summary>
        /// Удаляет Control из коллекции элементов приложения
        /// </summary>
        /// <param name="guid">Guid, присвоенный элементу при добавлении в коллекцию</param>
        public void Remove(Guid guid)
        {
            UIElementsCollection.Remove(guid);
        }
    }
}
