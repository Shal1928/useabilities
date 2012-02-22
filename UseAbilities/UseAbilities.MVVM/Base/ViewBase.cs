using System;
using System.Windows;

namespace UseAbilities.MVVM.Base
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
