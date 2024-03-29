﻿using CommunityToolkit.Mvvm.ComponentModel;
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
using WPFToolkit.NetCore.AuxiliaryTypes.DataGridColumns;

namespace WPFToolkit.NetCore.Controls
{
    /// <summary>
    /// Логика взаимодействия для ReportControl.xaml
    /// </summary>
    [INotifyPropertyChanged]
    public partial class ReportControl : UserControl
    {
        public static readonly DependencyProperty ContentGetterProperty = 
            DependencyProperty.Register("ContentGetter", typeof(Func<Task<DataTable>>), typeof(ReportControl));

        public static readonly DependencyProperty DataGridColumnsGetterProperty =
            DependencyProperty.Register("DataGridColumnsGetter", typeof(Func<ColumnTemplate[]>), typeof(ReportControl));

        public static readonly DependencyProperty IsBusyProperty = 
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(ReportControl), new PropertyMetadata(false));

        public static readonly DependencyProperty IsContentGridReadOnlyProperty =
            DependencyProperty.Register("IsContentGridReadOnly", typeof(bool), typeof(ReportControl), new PropertyMetadata(true));

        public static readonly DependencyProperty SelectedRowProperty =
            DependencyProperty.Register("SelectedRow", typeof(DataRowView), typeof(ReportControl));

        public static readonly DependencyProperty ContentGridContextMenuProperty =
            DependencyProperty.Register("ContentGridContextMenu", typeof(ContextMenu), typeof(ReportControl));

        public static readonly DependencyProperty BottomControlProperty =
            DependencyProperty.RegisterAttached("BottomControl", typeof(UIElement), typeof(ReportControl),
                new UIPropertyMetadata(null, BottomControlChanged));

        public static readonly DependencyProperty TopControlProperty =
            DependencyProperty.RegisterAttached("TopControl", typeof(UIElement), typeof(ReportControl),
                new UIPropertyMetadata(null, TopControlChanged));

        public static readonly DependencyProperty LeftControlProperty =
            DependencyProperty.RegisterAttached("LeftControl", typeof(UIElement), typeof(ReportControl),
                new UIPropertyMetadata(null, LeftControlChanged));

        public static readonly DependencyProperty RightControlProperty =
            DependencyProperty.RegisterAttached("RightControl", typeof(UIElement), typeof(ReportControl),
                new UIPropertyMetadata(null, RightControlChanged));

        public static readonly DependencyProperty RowFilterProperty = 
            DependencyProperty.RegisterAttached("RowFilter", typeof(string), typeof(ReportControl), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnRowFilterChanged)));

        /// <summary>
        /// Обработчик события изменения фильтра для содержимого DataGrid
        /// </summary>
        /// <param name="d">Элемент, вызвавший событие (Здесь это 100% ReportControl)</param>
        /// <param name="e">>Новое значение поля</param>
        static void OnRowFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ReportControl)d).ReportContent.DefaultView.RowFilter = e.NewValue.ToString();
        }

        /// <summary>
        /// Свойство, устанавливающее фильтр для содержимого DataGrid
        /// </summary>
        public string RowFilter
        {
            get { return (string)GetValue(RowFilterProperty); }
            set { SetValue(RowFilterProperty, value); }
        }

        /// <summary>
        /// Обработчик события изменения UIElement'а
        /// </summary>
        /// <param name="d">Элемент, вызвавший событие (Здесь это 100% ReportControl)</param>
        /// <param name="e">Новое значение поля</param>
        static void BottomControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ReportControl;
            if (control != null)
                control.BottomItemsControl.Items.Add(e.NewValue as UIElement);
        }

        /// <summary>
        /// Обработчик события изменения UIElement'а
        /// </summary>
        /// <param name="d">Элемент, вызвавший событие (Здесь это 100% ReportControl)</param>
        /// <param name="e">Новое значение поля</param>
        static void TopControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ReportControl;
            if (control != null)
                control.TopItemsControl.Items.Add(e.NewValue as UIElement);
        }

        /// <summary>
        /// Обработчик события изменения UIElement'а
        /// </summary>
        /// <param name="d">Элемент, вызвавший событие (Здесь это 100% ReportControl)</param>
        /// <param name="e">Новое значение поля</param>
        static void LeftControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ReportControl;
            if (control != null)
                control.LeftItemsControl.Items.Add(e.NewValue as UIElement);
        }

        /// <summary>
        /// Обработчик события изменения UIElement'а
        /// </summary>
        /// <param name="d">Элемент, вызвавший событие (Здесь это 100% ReportControl)</param>
        /// <param name="e">Новое значение поля</param>
        static void RightControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ReportControl;
            if (control != null)
                control.RightItemsControl.Items.Add(e.NewValue as UIElement);
        }

        /// <summary>
        /// Индикатор занятости контрола
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// Контейнер для элементов, находящихся внизу
        /// </summary>
        public UIElement BottomControl
        {
            get { return (UIElement)GetValue(BottomControlProperty); }
            set { SetValue(BottomControlProperty, value); }
        }

        /// <summary>
        /// Контейнер для элементов, находящихся слева
        /// </summary>
        public UIElement LeftControl
        {
            get { return (UIElement)GetValue(LeftControlProperty); }
            set { SetValue(LeftControlProperty, value); }
        }

        /// <summary>
        /// Контейнер для элементов, находящихся справа
        /// </summary>
        public UIElement RightControl
        {
            get { return (UIElement)GetValue(RightControlProperty); }
            set { SetValue(RightControlProperty, value); }
        }

        /// <summary>
        /// Контейнер для элементов, находящихся сверху
        /// </summary>
        public UIElement TopControl
        {
            get { return (UIElement)GetValue(TopControlProperty); }
            set { SetValue(TopControlProperty, value); }
        }

        /// <summary>
        /// Действие, выполняемое для получения DataTable 
        /// с данными для представления в DataGrid
        /// </summary>
        public Func<Task<DataTable>> ContentGetter
        {
            get { return (Func<Task<DataTable>>)GetValue(ContentGetterProperty); }
            set { SetValue(ContentGetterProperty, value); }
        }

        /// <summary>
        /// Действие, выполняемое для получения массива столбцов,
        /// отображаемых в DataGrid
        /// </summary>
        public Func<ColumnTemplate[]> DataGridColumnsGetter
        {
            get { return (Func<ColumnTemplate[]>)GetValue(DataGridColumnsGetterProperty); }
            set { SetValue(DataGridColumnsGetterProperty, value); }
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
        /// Выбранная строка DataGridView
        /// </summary>
        public DataRowView? SelectedRow
        {
            get { return (DataRowView?)GetValue(SelectedRowProperty); }
            set { SetValue(SelectedRowProperty, value); }
        }

        /// <summary>
        /// Контекстное меню для DataGrid
        /// </summary>
        public ContextMenu ContentGridContextMenu
        {
            get { return (ContextMenu)GetValue(ContentGridContextMenuProperty); }
            set { SetValue(ContentGridContextMenuProperty, value); }
        }

        /// <summary>
        /// Данные, отображаемые в DataGrid
        /// </summary>
        [ObservableProperty]
        DataTable reportContent = new();

        /// <summary>
        /// Универсальный метод для создания столбца для DataGrid
        /// </summary>
        /// <typeparam name="T">Тип столбца</typeparam>
        /// <param name="columnDescription">Описание свойств столбца (привязки и т.д.)</param>
        /// <returns>Столбец DataGrid</returns>
        static T GenerateColumn<T>(ColumnTemplate columnDescription) where T : DataGridBoundColumn, new()
        {
            var column = new T
            {
                Binding = new Binding(columnDescription.ColumnName),
                Header = columnDescription.Header,
                Width = DataGridLength.Auto
            };

            return column;
        }

        /// <summary>
        /// Метод, генерирующий коллекцию столбцов для DataGrid
        /// </summary>
        void UpdateColumns()
        {
            if (DataGridColumnsGetter == null) return;

            ContentGrid.Columns.Clear();
            foreach (var columnDescription in DataGridColumnsGetter.Invoke())
            {
                switch (columnDescription.ColumnType)
                {
                    case ColumnType.TEXT_COLUMN:
                        ContentGrid.Columns.Add(GenerateColumn<DataGridTextColumn>(columnDescription));
                        break;
                    case ColumnType.CHECKBOX_COLUMN:
                        ContentGrid.Columns.Add(GenerateColumn<DataGridCheckBoxColumn>(columnDescription));
                        break;
                }
            }
        }

        /// <summary>
        /// Метод, получающий содержимое для DataGrid
        /// </summary>
        async void UpdateContent()
        {
            if (ContentGetter == null) return;
            
            IsBusy = true;
            
            ReportContent = await ContentGetter.Invoke();
            RowFilter = string.Empty;
            
            IsBusy = false;
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

        private void Report_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
