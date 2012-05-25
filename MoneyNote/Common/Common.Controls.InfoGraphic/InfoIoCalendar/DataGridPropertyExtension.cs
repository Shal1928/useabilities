using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Common.Controls.InfoGraphic.InfoIoCalendar
{
    public class DataGridPropertyExtension
    {
        public static readonly DependencyProperty BindableColumnsProperty =
            DependencyProperty.RegisterAttached("BindableColumns",
                                                typeof(ObservableCollection<DataGridColumn>),
                                                typeof(DataGridPropertyExtension),
                                                new UIPropertyMetadata(null, BindableColumnsPropertyChanged));

        public static ObservableCollection<DataGridColumn> GetBindableColumns(DependencyObject element)
        {
            return (ObservableCollection<DataGridColumn>)element.GetValue(BindableColumnsProperty);
        }

        public static void SetBindableColumns(DependencyObject element, ObservableCollection<DataGridColumn> value)
        {
            element.SetValue(BindableColumnsProperty, value);
        }
        

        private static void BindableColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Debug.Assert(source is DataGrid);

            var dataGrid = source as DataGrid;
            var columns = e.NewValue as ObservableCollection<DataGridColumn>;

            dataGrid.Columns.Clear();
            if (columns == null) return;

            foreach (var column in columns)
                dataGrid.Columns.Add(column);
            
            columns.CollectionChanged += (sender, e2) =>
            {
                if (e2.Action == NotifyCollectionChangedAction.Reset)
                {
                    dataGrid.Columns.Clear();
                    foreach (DataGridColumn column in e2.NewItems)
                    {
                        dataGrid.Columns.Add(column);
                    }
                }
                else if (e2.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (DataGridColumn column in e2.NewItems)
                    {
                        dataGrid.Columns.Add(column);
                    }
                }
                else if (e2.Action == NotifyCollectionChangedAction.Move)
                {
                    dataGrid.Columns.Move(e2.OldStartingIndex, e2.NewStartingIndex);
                }
                else if (e2.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (DataGridColumn column in e2.OldItems)
                    {
                        dataGrid.Columns.Remove(column);
                    }
                }
                else if (e2.Action == NotifyCollectionChangedAction.Replace)
                {
                    dataGrid.Columns[e2.NewStartingIndex] = e2.NewItems[0] as DataGridColumn;
                }
            };
        }
    }
}
