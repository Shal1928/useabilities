using System;
using System.Windows;
using Common.MVVM.Core.Base;

namespace Common.Code.UseAbilities.MVVM.Base
{
    public class ViewBase : Window
    {
        protected ViewBase()
        {
            Closed += OnViewClosed;
        }

        private void OnViewClosed(object sender, EventArgs e)
        {
            var dataContext = DataContext as ViewModelBase;
            if (dataContext != null) dataContext.Close();
        }
    }
}
