using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFToolkit.NetCore.AuxiliaryTypes;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.Converters;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;
using WPFToolkit.NetCore.AuxiliaryTypes.TextBoxes;
using WPFToolkit.NetCore.Controls;

namespace WPFToolkit.NetCore.UIManagers
{
    public class UIElementsDecorator
    {
        #region Constants
        const double DEFAULT_BUTTON_LEFT_PADDING = 20;
        const double DEFAULT_BUTTON_RIGHT_PADDING = 20;
        const double DEFAULT_BUTTON_TOP_PADDING = 5;
        const double DEFAULT_BUTTON_BOTTOM_PADDING = 5;

        const double DEFAULT_MENUITEM_LEFT_PADDING = 7;
        const double DEFAULT_MENUITEM_RIGHT_PADDING = 7;
        const double DEFAULT_MENUITEM_TOP_PADDING = 2;
        const double DEFAULT_MENUITEM_BOTTOM_PADDING = 2;

        const double DEFAULT_LEFT_MARGIN = 5;
        const double DEFAULT_RIGHT_MARGIN = 5;
        const double DEFAULT_TOP_MARGIN = 5;
        const double DEFAULT_BOTTOM_MARGIN = 5;

        const double DEFAULT_BORDER_THICKNESS = 1.5;
        #endregion

        #region Auxiliary methods
        /// <summary>
        /// Предназначен для создания DataGridColumn определенного типа
        /// </summary>
        /// <typeparam name="T">Тип создаваемого столбца</typeparam>
        /// <param name="header">Заголовок</param>
        /// <param name="columnName">Имя стобца, к которому будет выполняться привязка</param>
        /// <returns></returns>
        static T GenerateColumn<T>(string header, string columnName) where T : DataGridBoundColumn, new()
        {
            var column = new T
            {
                Binding = new Binding(columnName),
                Header = header,
                Width = DataGridLength.Auto                
            };
            return column;
        }
        /// <summary>
        /// Предназначен для создания коллекции элементов Menu
        /// </summary>
        /// <param name="templates">Коллекция описаний элементов меню</param>
        /// <returns></returns>
        static List<MenuItem>? GenerateMenuItemsCollection(IEnumerable<MenuItemTemplate>? templates)
        {
            if (templates == null) return null;
            var result = new List<MenuItem>();
            foreach (var item in templates)
            {
                var menuItem = new MenuItem()
                {
                    Header = item.Header,
                    Command = item.Command,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(DEFAULT_MENUITEM_LEFT_PADDING, DEFAULT_MENUITEM_TOP_PADDING, DEFAULT_MENUITEM_RIGHT_PADDING, DEFAULT_MENUITEM_BOTTOM_PADDING),
                    ItemsSource = GenerateMenuItemsCollection(item.SubItems)
                };
                if (item.IconPath != null)
                {
                    menuItem.Icon = new Image()
                    {
                        Source = new BitmapImage(item.IconPath),
                    };
                }
                result.Add(menuItem);
            }
            return result;
        }
        #endregion

        #region Creation methods
        /// <summary>
        /// Создает Button и добавляет в коллекцию UI элементов
        /// </summary>
        /// <param name="content">Текст кнопки</param>
        /// <param name="command">Команда, выполняемая при нажатии</param>
        /// <param name="color">Цвет кнопки</param>
        /// <returns></returns>
        public static Guid CreateButton(string content, ICommand? command = null, Color? color = null, string? isEnabledPropertyName = null)
        {
            var button = new Button();
            button.Content = content;
            button.Padding = new Thickness(DEFAULT_BUTTON_LEFT_PADDING, DEFAULT_BUTTON_TOP_PADDING, DEFAULT_BUTTON_RIGHT_PADDING, DEFAULT_BUTTON_BOTTOM_PADDING);
            button.Command = command;
            button.BorderThickness = new Thickness(DEFAULT_BORDER_THICKNESS);
            button.Margin = new Thickness(DEFAULT_LEFT_MARGIN, DEFAULT_TOP_MARGIN, DEFAULT_RIGHT_MARGIN, DEFAULT_BOTTOM_MARGIN);
            if (color.HasValue) button.Background = new SolidColorBrush(color.Value);
            if (!string.IsNullOrEmpty(isEnabledPropertyName)) button.SetBinding(Button.IsEnabledProperty, new Binding(isEnabledPropertyName));
            button.HorizontalContentAlignment = HorizontalAlignment.Center;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            return UIElementsManager<Control>.Instance.Add(button);
        }
        /// <summary>
        /// Создает DataGrid и добавляет в коллекцию UI элементов
        /// </summary>
        /// <param name="columnTemplatesCollection">Коллекция описаний столбцов</param>
        /// <returns>Guid, присвоенный созданному элементу</returns>
        public static Guid CreateDataGrid(IEnumerable<ColumnTemplate> columnTemplatesCollection, string? sourcePropertyName = "", string? selectedItemPropertyName = "", Guid? contextMenuGuid = null)
        {
            DataGrid dataGrid = new()
            {
                AutoGenerateColumns = false,
                IsReadOnly = true,
                CanUserAddRows = false,
                CanUserDeleteRows = false,
                ContextMenu = contextMenuGuid.HasValue ? (ContextMenu)UIElementsManager<Control>.Instance.Get(contextMenuGuid.Value) : null
            };
            if (!string.IsNullOrEmpty(sourcePropertyName))
            {
                dataGrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding(sourcePropertyName));
            }
            if (!string.IsNullOrEmpty(selectedItemPropertyName))
            {
                //dataGrid.SetBinding(DataGrid.SelectedItemProperty, new Binding(selectedItemPropertyName));
                dataGrid.SetBinding(DataGrid.SelectedItemProperty, new Binding(selectedItemPropertyName)
                {
                    Converter = new DataRowViewToRowConverter(),
                });
            }
            foreach (var template in columnTemplatesCollection)
            {
                switch (template.ColumnType)
                {
                    case ColumnType.TEXT_COLUMN:
                        dataGrid.Columns.Add(GenerateColumn<DataGridTextColumn>(template.Header, template.ColumnName));
                        break;
                    case ColumnType.CHECKBOX_COLUMN:
                        dataGrid.Columns.Add(GenerateColumn<DataGridCheckBoxColumn>(template.Header, template.ColumnName));
                        break;
                }
            }
            return UIElementsManager<Control>.Instance.Add(dataGrid);
        }
        /// <summary>
        /// Создает Menu и добавляет в коллекцию элементов
        /// </summary>
        /// <param name="templates">Коллекция описаний элементов меню</param>
        /// <returns>Guid, присвоенный созданному элементу</returns>
        public static Guid CreateMenu(IEnumerable<MenuItemTemplate> templates, string? header = null)
        {
            List<MenuItem>? menuItems = GenerateMenuItemsCollection(templates);
            if (!string.IsNullOrEmpty(header))
            {
                MenuItem rootMenuItem = new()
                {
                    Header = header,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(DEFAULT_MENUITEM_LEFT_PADDING, DEFAULT_MENUITEM_TOP_PADDING, DEFAULT_MENUITEM_RIGHT_PADDING, DEFAULT_MENUITEM_BOTTOM_PADDING),
                    ItemsSource = menuItems,
                };
                return UIElementsManager<Control>.Instance.Add(rootMenuItem);
            }           
            else
            {
                ContextMenu menu = new()
                {
                    ItemsSource = menuItems
                };
                return UIElementsManager<Control>.Instance.Add(menu);
            }
        }
        /// <summary>
        /// Создает поле для ввода с заголовком
        /// </summary>
        /// <param name="label">Заголовок поля для ввода</param>
        /// <param name="textPropertyName">Имя поля, к которому будет привязано свойство Text элемента управления</param>
        /// <param name="handler">Обработчик события изменения текста</param>
        /// <returns></returns>
        public static Guid CreateMarkedTextBox(string label, string? textPropertyName, string? isEnabledPropertyName = null, TextChangedEventHandler? handler = null, UpdateSourceTrigger updateSourceTrigger = UpdateSourceTrigger.PropertyChanged)
        {
            var textBox = new MarkedTextBoxControl();
            textBox.Margin = new Thickness(DEFAULT_LEFT_MARGIN, DEFAULT_TOP_MARGIN, DEFAULT_RIGHT_MARGIN, DEFAULT_BOTTOM_MARGIN);
            textBox.Label = label;
            if (!string.IsNullOrEmpty(textPropertyName))
            {
                textBox.TextBox.SetBinding(TextBox.TextProperty, new Binding(textPropertyName)
                {
                    UpdateSourceTrigger = updateSourceTrigger,
                });
            }
            if (!string.IsNullOrEmpty(isEnabledPropertyName))
            {
                textBox.SetBinding(MarkedTextBoxControl.IsEnabledProperty, new Binding(isEnabledPropertyName));
            }
            if (handler != null)
            {
                textBox.TextChanged = handler;
            }
            return UIElementsManager<Control>.Instance.Add(textBox);
        }
        #endregion
    }
}
