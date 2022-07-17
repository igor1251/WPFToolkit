using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFToolkit.NetCore.AuxiliaryTypes;

namespace WPFToolkit.NetCore.Controls
{
    /// <summary>
    /// Логика взаимодействия для ReportControl.xaml
    /// </summary>
    public partial class ReportControl : UserControl
    {
        public static readonly DependencyProperty ContentGetterProperty = 
            DependencyProperty.Register("ContentGetter", typeof(Func<DataTable>), typeof(ReportControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DataGridColumnsGetterProperty =
            DependencyProperty.Register("DataGridColumnsGetter", typeof(Func<DataGridColumnDescription[]>), typeof(ReportControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IsBusyProperty = 
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(ReportControl), new PropertyMetadata(false));

        public static readonly DependencyProperty IsContentGridReadOnlyProperty =
            DependencyProperty.Register("IsContentGridReadOnly", typeof(bool), typeof(ReportControl), new PropertyMetadata(true));

        /// <summary>
        /// Действие, выполняемое для получения DataTable 
        /// с данными для представления в DataGrid
        /// </summary>
        public Func<DataTable> ContentGetter
        {
            get { return (Func<DataTable>)GetValue(ContentGetterProperty); }
            set { SetValue(ContentGetterProperty, value); }
        }

        /// <summary>
        /// Действие, выполняемое для получения массива столбцов,
        /// отображаемых в DataGrid
        /// </summary>
        public Func<DataGridColumnDescription[]> DataGridColumnsGetter
        {
            get { return (Func<DataGridColumnDescription[]>)GetValue(DataGridColumnsGetterProperty); }
            set { SetValue(DataGridColumnsGetterProperty, value); }
        }

        /// <summary>
        /// Индикатор исполнения какой-либо операии в элементе управления
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// Доступны ячейки DataGrid для редактирования или нет
        /// </summary>
        public bool IsContentGridReadOnly
        {
            get { return (bool)GetValue(IsContentGridReadOnlyProperty); }
            set { SetValue(IsContentGridReadOnlyProperty, value); }
        }

        /// <summary>
        /// Данные, отображаемые в DataGrid
        /// </summary>
        public DataTable ReportContent { get; set; } = new DataTable();

        /// <summary>
        /// Выбранная строка DataGrid
        /// </summary>
        public object? SelectedRow { get; set; }

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

        void UpdateColumns()
        {
            ContentGrid.Columns.Clear();
            foreach (var columnDescription in DataGridColumnsGetter.Invoke())
            {
                switch (columnDescription.ColumnType)
                {
                    case DataGridColumnType.TEXT_COLUMN:
                        ContentGrid.Columns.Add(GenerateColumn<DataGridTextColumn>(columnDescription));
                        break;
                    case DataGridColumnType.CHECKBOX_COLUMN:
                        ContentGrid.Columns.Add(GenerateColumn<DataGridCheckBoxColumn>(columnDescription));
                        break;
                }
            }
        }

        void UpdateContent()
        {
            ReportContent = ContentGetter.Invoke();
        }

        /// <summary>
        /// Метод, необходимый для обновления содержимого отчета
        /// </summary>
        public void Update()
        {
            UpdateContent();
            UpdateColumns();
        }

        public ReportControl()
        {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
