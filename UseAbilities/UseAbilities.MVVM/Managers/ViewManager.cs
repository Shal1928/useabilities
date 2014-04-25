using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using UseAbilities.Extensions.Dictionaries;
using UseAbilities.MVVM.Base;

namespace UseAbilities.MVVM.Managers
{
    public class ViewManager
    {
        #region Data

        private readonly Dictionary<Type, Type> _relations = new Dictionary<Type, Type>();
        private readonly List<Window> _openedViews = new List<Window>();

        #endregion

        #region Singleton implementation

        protected ViewManager()
        {
            //
        }

        private static readonly ViewManager SingleInstance = new ViewManager();
        public static ViewManager Instance
        {
            get { return SingleInstance; }
        }

        #endregion

        public void OnViewModelsCoolectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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

        private void ShowView(ViewModelBase viewModel2Show)
        {
            if (!_relations.ContainsKey(viewModel2Show.GetType())) return;

            var newWindowType = _relations[viewModel2Show.GetType()];
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

        private void CloseView(ViewModelBase viewModel2Close)
        {
            foreach (var openedView in _openedViews.Where(openedView => openedView.DataContext == viewModel2Close))
            {
                openedView.Close();
                _openedViews.Remove(openedView);
                return;
            }
        }

        private void CloseAllViews()
        {
            foreach (var openedView in _openedViews)
                openedView.Close();
            _openedViews.Clear();
        }

        public virtual void RegisterRelation<TViewModel, TView>()
        {
            _relations.Put(typeof(TViewModel), typeof(TView));
        }

        public virtual void RegisterRelation(Type viewModelType, Type viewType)
        {
            _relations.Put(viewModelType, viewType);
        }

        public void RegisterRelations(Dictionary<Type, Type> relations)
        {
            foreach (var keyType in relations.Keys)
                RegisterRelation(keyType, relations[keyType]);
        }
    }
}
