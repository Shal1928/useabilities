using System.Collections.ObjectModel;
using UseAbilities.MVVM.Base;

namespace UseAbilities.MVVM.Managers
{
    public static class ViewModelManager
    {
        private static readonly ObservableCollection<ViewModelBase> _activeViewModels = new ObservableCollection<ViewModelBase>();

        public static ObservableCollection<ViewModelBase> ActiveViewModels
        {
            get
            {
                return _activeViewModels;
            }
        }

        public static void RegisterViewModel(ViewModelBase viewModelBase)
        {
            _activeViewModels.Add(viewModelBase);
        }

        public static void RemoveViewModel(ViewModelBase viewModelBase)
        {
            _activeViewModels.Remove(viewModelBase);
        }
    }
}
