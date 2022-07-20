using CommunityToolkit.Mvvm.ComponentModel;
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
        DataTable reportContent = new DataTable();

        [ObservableProperty]
        bool isBusy = false;

        [ObservableProperty]
        string rowFilter = string.Empty;
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
        /// Метод, генерирующий коллекцию столбцов для DataGrid
        /// </summary>
        void UpdateColumns()
        {
            if (ViewModel.DataGridColumnsGetter == null) return;

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
            if (ViewModel.ReportContentGetter == null) return;

            IsBusy = true;

            ReportContent = await ViewModel.ReportContentGetter.Invoke();
            RowFilter = ViewModel.RowFilterGetter?.Invoke() ?? string.Empty;

            IsBusy = false;
        }

        void UpdateCaption()
        {
            Caption = ViewModel.ViewCaptionGetter?.Invoke() ?? string.Empty;
        }

        void Update()
        {
            if (ViewModel == null) return;
            UpdateCaption();
            UpdateContent();
            UpdateColumns();
        }

        void SetupEventHandlers()
        {
            if (ViewModel != null)
                if (ViewModel.RowDoubleClicked != null)
                    DataGrid.MouseDoubleClick += ViewModel.RowDoubleClicked;
        }
        #endregion

        #region EventHandlers
        void Report_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
            SetupEventHandlers();
        }
        #endregion

        #region Constructors
        public ReportWindow()
        {
            InitializeComponent();
        }

        public ReportWindow(IViewModel viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
        }
        #endregion
    }
}
