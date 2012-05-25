using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Common.MVVM.Core.Base;

namespace Common.Controls.InfoGraphic.InfoIoCalendar
{
    internal class InfoIoCalendarViewModel : ObserveProperty
    {
        private ObservableCollection<DataGridColumn> _columnCollection;
        public ObservableCollection<DataGridColumn> ColumnCollection
        {
            get
            {
                return _columnCollection;
            }
            set
            {
                _columnCollection = value;
                OnPropertyChanged(() => ColumnCollection);
            }
        }


    }
}
