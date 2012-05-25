using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Common.MVVM.Core.Base;

namespace Common.MVVM.Core.Managers
{
    public static class ViewManager
    {
        #region Data

        private static readonly Dictionary<Type, Type> _mapping = new Dictionary<Type, Type>();
        private static readonly List<Window> _openedViews = new List<Window>();

        #endregion

        public static void OnViewModelsCoolectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (ViewModelBase newViewModel in e.NewItems)
                        ShowView(newViewModel);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (ViewModelBase newViewModel in e.OldItems)
                        CloseView(newViewModel);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    throw new NotSupportedException("NotifyCollectionChangedAction.Replace is not supported");

                case NotifyCollectionChangedAction.Reset:
                    CloseAllViews();
                    foreach (var newViewModel in (IEnumerable<ViewModelBase>)sender)
                        ShowView(newViewModel);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(@"Unknown Notify CollectionChangedEventArgs!", (Exception)null);
            }
        }

        public static void ShowView(ViewModelBase viewModel2Show)
        {
            if (!_mapping.ContainsKey(viewModel2Show.GetType())) return;

            var newWindowType = _mapping[viewModel2Show.GetType()];
            var newWindow = newWindowType.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes) as Window;

            if (newWindow == null) return;

            newWindow.DataContext = viewModel2Show;
            _openedViews.Add(newWindow);

            if (viewModel2Show.IsModal)
            {
                newWindow.ShowDialog();
                return;
            }

            newWindow.Show();
        }

        private static void CloseView(ViewModelBase viewModel2Close)
        {
            foreach (var openedView in _openedViews.Where(openedView => openedView.DataContext == viewModel2Close))
            {
                openedView.Close();
                _openedViews.Remove(openedView);
                return;
            }
        }

        private static void CloseAllViews()
        {
            foreach (var openedView in _openedViews)
                openedView.Close();
            _openedViews.Clear();
        }

        public static void RegisterViewViewModelRelation(Type viewModelType, Type viewType)
        {
            if (_mapping.ContainsKey(viewModelType))
                _mapping[viewModelType] = viewType;
            else _mapping.Add(viewModelType, viewType);
        }

        public static void RegisterViewViewModelRelations(Dictionary<Type, Type> relations)
        {
            foreach (var keyType in relations.Keys)
                RegisterViewViewModelRelation(keyType, relations[keyType]);
        }
    }
}
