using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFToolkit.NetCore.AuxiliaryTypes;
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;
using WPFToolkit.NetCore.AuxiliaryTypes.Menus;
using WPFToolkit.NetCore.AuxiliaryTypes.ViewModels;

namespace WPFToolkit.NetCore.Controls
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    [INotifyPropertyChanged]
    public partial class ReportWindow : Window
    {
        #region Dependency properties
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(IViewModel), typeof(ReportWindow));
        /// <summary>
        /// ViewModel для отчета
        /// </summary>
        public IViewModel ViewModel
        {
            get { return (IViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        #endregion

        #region Properties
        [ObservableProperty]
        string caption = string.Empty;

        [ObservableProperty]
        DataTable reportContent = new();

        [ObservableProperty]
        string rowFilter = string.Empty;

        [ObservableProperty]
        bool isBusy = false;

        [ObservableProperty]
        List<MenuItem>? windowMenuItems;

        [ObservableProperty]
        List<MenuItem>? dataGridContextMenuItems;

        [ObservableProperty]
        bool windowMenuEnabled = false;

        [ObservableProperty]
        bool dataGridContextMenuEnabled = false;
        #endregion

        #region Commands
        /// <summary>
        /// Команда обновления содержимого окна
        /// </summary>
        [ICommand]
        void Update()
        {
            UpdateCaption();
            UpdateContent();
            UpdateColumns();
        }
        #endregion

        #region Auxiliary methods
        /// <summary>
        /// Универсальный метод для создания столбца для DataGrid
        /// </summary>
        /// <typeparam name="T">Тип столбца</typeparam>
        /// <param name="columnDescription">Описание свойств столбца (привязки и т.д.)</param>
        /// <returns>Столбец DataGrid</returns>
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
        /// Метод, валидирующий свойства ViewModel
        /// </summary>
        /// <param name="viewModel">ViewModel для окна</param>
        /// <returns></returns>
        bool ValidateViewModel(IViewModel viewModel)
        {
            bool validationResult = false;

            if (viewModel != null)
            {
                validationResult = viewModel.ReportContentGetter != null &&
                                   viewModel.DataGridColumnsGetter != null &&
                                   viewModel.ViewCaptionGetter != null;
            }            
            return validationResult;
        }
        List<MenuItem> GenerateMenuItemsCollection(IEnumerable<MenuItemDescription>? descriptions)
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
                    ItemsSource = GenerateMenuItemsCollection(item.SubItems)
                };
                result.Add(menuItem);
            }
            return result;
        }
        #endregion

        #region Content setuppers
        /// <summary>
        /// Метод, генерирующий коллекцию столбцов для DataGrid
        /// </summary>
        void UpdateColumns()
        {
            DataGrid.Columns.Clear();
            foreach (var columnDescription in ViewModel.DataGridColumnsGetter.Invoke())
            {
                switch (columnDescription.ColumnType)
                {
                    case DataGridColumnType.TEXT_COLUMN:
                        DataGrid.Columns.Add(GenerateColumn<DataGridTextColumn>(columnDescription));
                        break;
                    case DataGridColumnType.CHECKBOX_COLUMN:
                        DataGrid.Columns.Add(GenerateColumn<DataGridCheckBoxColumn>(columnDescription));
                        break;
                }
            }
        }
        /// <summary>
        /// Метод, получающий содержимое для DataGrid
        /// </summary>
        async void UpdateContent()
        {
            IsBusy = true;

            ReportContent = await ViewModel.ReportContentGetter.Invoke();
            RowFilter = ViewModel.RowFilterGetter?.Invoke() ?? string.Empty;

            IsBusy = false;
        }
        /// <summary>
        /// Метод, обновляющий заголовок окна
        /// </summary>
        void UpdateCaption()
        {
            Caption = ViewModel.ViewCaptionGetter?.Invoke() ?? string.Empty;
        }
        /// <summary>
        /// Метод настройки событий
        /// </summary>
        void SetupEventHandlers()
        {
            if (ViewModel.RowDoubleClicked != null)
                DataGrid.MouseDoubleClick += ViewModel.RowDoubleClicked;
        }
        /// <summary>
        /// Метод настройки контекстного меню DataGrid
        /// </summary>
        void SetupDataGridContextMenu()
        {
            if (ViewModel.ContextMenuItemsGetter == null) return;
            DataGridContextMenuItems = GenerateMenuItemsCollection(ViewModel.ContextMenuItemsGetter.Invoke());
            DataGrid.ContextMenu = new ContextMenu();
            DataGrid.ContextMenu.ItemsSource = DataGridContextMenuItems;
            if (DataGridContextMenuItems.Count > 0) DataGridContextMenuEnabled = true;
            DataGrid.ContextMenu.IsEnabled = DataGridContextMenuEnabled;
        }
        /// <summary>
        /// Метод настройки меню окна
        /// </summary>
        void SetupWindowMenu()
        {
            if (ViewModel.WindowMenuItemsGetter == null) return;
            WindowMenuItems = GenerateMenuItemsCollection(ViewModel.WindowMenuItemsGetter.Invoke());
            if (WindowMenuItems.Count > 0) WindowMenuEnabled = true;
        }
        /// <summary>
        /// Метод наполнения кнопками
        /// </summary>
        void SetupButtons()
        {
            if (ViewModel.ButtonsGetter == null) return;
            foreach (var item in ViewModel.ButtonsGetter.Invoke())
            {
                var button = new Button();
                button.Content = item.Content;
                button.Command = item.Command;
                button.BorderThickness = new Thickness(1.5);
                button.Margin = new Thickness(5, 5, 5, 5);
                button.HorizontalContentAlignment = HorizontalAlignment.Center;
                button.VerticalContentAlignment = VerticalAlignment.Center;
                switch (item.Location)
                {
                    case AuxiliaryTypes.Universal.UIElementLocation.TOP:
                        TopControlsPanel.Children.Add(button);
                        break;
                    case AuxiliaryTypes.Universal.UIElementLocation.LEFT:
                        LeftControlsPanel.Children.Add(button);
                        break;
                    case AuxiliaryTypes.Universal.UIElementLocation.RIGHT:
                        RightControlsPanel.Children.Add(button);
                        break;
                    case AuxiliaryTypes.Universal.UIElementLocation.BOTTOM:
                        BottomControlsPanel.Children.Add(button);
                        break;
                }
            }
        }
        /// <summary>
        /// Метод наполнения полями для ввода
        /// </summary>
        void SetupEntries()
        {
            if (ViewModel.EntriesGetter == null) return;
            foreach (var item in ViewModel.EntriesGetter.Invoke())
            {
                var entry = new MarkedTextBoxControl();
                entry.Margin = new Thickness(5, 5, 5, 5);
                entry.Label = item.Label;
                entry.TextChanged = item.TextChanged;
                switch (item.Location)
                {
                    case AuxiliaryTypes.Universal.UIElementLocation.TOP:
                        TopControlsPanel.Children.Add(entry);
                        break;
                    case AuxiliaryTypes.Universal.UIElementLocation.LEFT:
                        LeftControlsPanel.Children.Add(entry);
                        break;
                    case AuxiliaryTypes.Universal.UIElementLocation.RIGHT:
                        RightControlsPanel.Children.Add(entry);
                        break;
                    case AuxiliaryTypes.Universal.UIElementLocation.BOTTOM:
                        BottomControlsPanel.Children.Add(entry);
                        break;
                }
            }
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Обработчик события, при котором элементы управления загружены и готовы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Report_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
            SetupEventHandlers();
            SetupDataGridContextMenu();
            SetupWindowMenu();
            SetupDataGridContextMenu();
            SetupButtons();
            SetupEntries();
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Главный конструктор формы
        /// </summary>
        /// <param name="viewModel">ViewModel для формы</param>
        /// <exception cref="ArgumentException">Возникает при неправильно настроенной ViewModel</exception>
        public ReportWindow(IViewModel viewModel)
        {
            if (!ValidateViewModel(viewModel))
                throw new ArgumentException("Свойства ViewModel не установлены правильно");
            InitializeComponent();
            this.ViewModel = viewModel;
        }
        #endregion
    }
}
