using System.Windows;

namespace UseAbilities.WPF.Controls.DataGrid
{
    public class AutoGenerateColumn
    {
        public string Column
        {
            get;
            set;
        }

        public DataTemplate CellTemplate
        {
            get;
            set;
        }

        public DataTemplate CellEditingTemplate
        {
            get;
            set;
        }

        public System.Windows.Data.BindingBase Binding
        {
            get;
            set;
        }
    }
}
