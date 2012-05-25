using System;
using System.Collections.Generic;
using System.Windows;
using Application.MoneyNote.ViewModels;
using Application.MoneyNote.Views;
using Common.MVVM.Core.Managers;

namespace Application.MoneyNote
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var relationsViewToViewModel = new Dictionary<Type, Type>
                                         {
                                            {typeof (MainWindowViewModel), typeof (MainWindowView)}
                                         };

            ViewManager.RegisterViewViewModelRelations(relationsViewToViewModel);
            ViewModelManager.ActiveViewModels.CollectionChanged += ViewManager.OnViewModelsCoolectionChanged;

            var startupWindow = new MainWindowViewModel();
            startupWindow.Show();
        }
    }
}
