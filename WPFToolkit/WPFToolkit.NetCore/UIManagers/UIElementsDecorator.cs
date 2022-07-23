using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WPFToolkit.NetCore.AuxiliaryTypes;
using WPFToolkit.NetCore.AuxiliaryTypes.Buttons;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;
using WPFToolkit.NetCore.AuxiliaryTypes.TextBoxes;
using WPFToolkit.NetCore.Controls;

namespace WPFToolkit.NetCore.UIManagers
{
    public class UIElementsDecorator
    {
        #region Constants
        const double BUTTON_LEFT_PADDING = 20;
        const double BUTTON_RIGHT_PADDING = 20;
        const double BUTTON_TOP_PADDING = 5;
        const double BUTTON_BOTTOM_PADDING = 5;

        const double MENUITEM_LEFT_PADDING = 7;
        const double MENUITEM_RIGHT_PADDING = 7;
        const double MENUITEM_TOP_PADDING = 2;
        const double MENUITEM_BOTTOM_PADDING = 2;

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
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="columnDescription">Описание столбца</param>
        /// <returns></returns>
        static T GenerateColumn<T>(DataGridColumnDescription columnDescription) where T : DataGridBoundColumn, new()
        {
            var column = new T
            {
                Binding = new Binding(columnDescription.InDatabaseName),
                Header = columnDescription.DisplayName,
                Width = DataGridLength.Auto                
            };
            return column;
        }
        /// <summary>
        /// Предназначен для создания коллекции элементов Menu
        /// </summary>
        /// <param name="descriptions">Коллекция описаний элементов меню</param>
        /// <returns></returns>
        static List<MenuItem> GenerateMenuItemsCollection(IEnumerable<MenuItemDescription>? descriptions)
        {
            if (descriptions == null) return null;
            var result = new List<MenuItem>();
            foreach (var item in descriptions)
            {
                var menuItem = new MenuItem()
                {
                    Header = item.Header,
                    Command = item.Command,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(MENUITEM_LEFT_PADDING, MENUITEM_TOP_PADDING, MENUITEM_RIGHT_PADDING, MENUITEM_BOTTOM_PADDING),
                    ItemsSource = GenerateMenuItemsCollection(item.SubItems)
                };
                result.Add(menuItem);
            }
            return result;
        }
        #endregion

        #region Creation methods
        /// <summary>
        /// Создает Button и добавляет в коллекцию UI элементов
        /// </summary>
        /// <param name="description">Описание кнопки</param>
        /// <returns>Guid, присвоенный созданному элементу</returns>
        public static Guid CreateButton(ButtonDescription description)
        {
            var button = new Button();
            button.Content = description.Content;
            button.Padding = new Thickness(BUTTON_LEFT_PADDING, BUTTON_TOP_PADDING, BUTTON_RIGHT_PADDING, BUTTON_BOTTOM_PADDING);
            button.Command = description.Command;
            button.BorderThickness = new Thickness(DEFAULT_BORDER_THICKNESS);
            button.Margin = new Thickness(DEFAULT_LEFT_MARGIN, DEFAULT_TOP_MARGIN, DEFAULT_RIGHT_MARGIN, DEFAULT_BOTTOM_MARGIN);
            if (description.BackgroundColor != null) button.Background = new SolidColorBrush(description.BackgroundColor.Value);
            button.HorizontalContentAlignment = HorizontalAlignment.Center;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            return UIElementsManager<Control>.Instance.Add(button);
        }
        /// <summary>
        /// Создает DataGrid и добавляет в коллекцию UI элементов
        /// </summary>
        /// <param name="columnDescriptions">Коллекция описаний столбцов</param>
        /// <returns>Guid, присвоенный созданному элементу</returns>
        public static Guid CreateDataGrid(IEnumerable<DataGridColumnDescription> columnDescriptions, DataTable? Content = null)
        {
            DataGrid dataGrid = new();
            dataGrid.AutoGenerateColumns = false;
            dataGrid.IsReadOnly = true;
            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserDeleteRows = false;
            if (Content != null)
            {
                dataGrid.ItemsSource = Content.DefaultView;
            }
            foreach (var description in columnDescriptions)
            {
                switch (description.ColumnType)
                {
                    case DataGridColumnType.TEXT_COLUMN:
                        dataGrid.Columns.Add(GenerateColumn<DataGridTextColumn>(description));
                        break;
                    case DataGridColumnType.CHECKBOX_COLUMN:
                        dataGrid.Columns.Add(GenerateColumn<DataGridCheckBoxColumn>(description));
                        break;
                }
            }
            return UIElementsManager<Control>.Instance.Add(dataGrid);
        }
        /// <summary>
        /// Создает Menu и добавляет в коллекцию элементов
        /// </summary>
        /// <param name="descriptions">Коллекция описаний элементов меню</param>
        /// <returns>Guid, присвоенный созданному элементу</returns>
        public static Guid CreateMenu(IEnumerable<MenuItemDescription> descriptions, string header = null)
        {           
            MenuItem rootMenuItem = new()
            {
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Padding = new Thickness(MENUITEM_LEFT_PADDING, MENUITEM_TOP_PADDING, MENUITEM_RIGHT_PADDING, MENUITEM_BOTTOM_PADDING),
                ItemsSource = GenerateMenuItemsCollection(descriptions)
            };
            if (!string.IsNullOrEmpty(header)) rootMenuItem.Header = header;
            return UIElementsManager<Control>.Instance.Add(rootMenuItem);
        }
        /// <summary>
        /// Создает MarkedTextBox и добавляет в коллекцию элементов
        /// </summary>
        /// <param name="description"></param>
        /// <returns>Guid, присвоенный созданному элементу</returns>
        public static Guid CreateMarkedTextBox(MarkedTextBoxDescription description)
        {
            var textBox = new MarkedTextBoxControl();
            textBox.Margin = new Thickness(DEFAULT_LEFT_MARGIN, DEFAULT_TOP_MARGIN, DEFAULT_RIGHT_MARGIN, DEFAULT_BOTTOM_MARGIN);
            textBox.Label = description.Label;
            textBox.TextBox.SetBinding(TextBox.TextProperty, description.Binding);
            if (description.TextChanged != null)
            {
                textBox.TextChanged = description.TextChanged;
            }
            return UIElementsManager<Control>.Instance.Add(textBox);
        }
        /// <summary>
        /// Создает индикатор занятости
        /// </summary>
        /// <returns>Guid, присвоенный созданному элементу</returns>
        public static Guid CreateBusyControl()
        {
            BusyControl busyControl = new();
            return UIElementsManager<Control>.Instance.Add(busyControl);
        }
        #endregion
    }
}
