using System;
using System.Collections.Generic;
using UseAbilities.Extensions.Dictionaries;
using UseAbilities.IoC.Helpers;
using UseAbilities.MVVM.Base;

namespace UseAbilities.MVVM.Managers
{
    public class AdvancedViewManager : ViewManager
    {
        private Dictionary<Type, Type> WrappedViewModels { get; set; } 

        #region Singleton implementation

        private AdvancedViewManager()
        {
            WrappedViewModels = new Dictionary<Type, Type>();
            ViewModelManager.ActiveViewModels.CollectionChanged += OnViewModelsCoolectionChanged;
        }

        private static readonly AdvancedViewManager SingleInstance = new AdvancedViewManager();
        public new static AdvancedViewManager Instance
        {
            get { return SingleInstance; }
        }

        #endregion

        

        private void RegisterWrap<T>()
        {
            RegisterWrap(typeof (T));
        }

        private void RegisterWrap(Type type)
        {
            WrappedViewModels.Put(type, Wrap(type));
        }



        private Type GetWrappedType<T>(bool isNewAdd = false)
        {
            return GetWrappedType(typeof(T), isNewAdd);
        }

        private Type GetWrappedType(Type type, bool isNewAdd = false)
        {
            if (WrappedViewModels.ContainsKey(type)) return WrappedViewModels[type];
            if (!isNewAdd) return null;

            RegisterWrap(type);
            return WrappedViewModels[type];
        }


        private static Type Wrap<T>()
        {
            return Wrap(typeof(T));
        }

        private static Type Wrap(Type type)
        {
            return ObserveWrapper.Wrap(type);
        }




        public override void RegisterRelation<TViewModel, TView>()
        {
            RegisterRelation(typeof(TViewModel), typeof(TView));
        }

        public override void RegisterRelation(Type viewModelType, Type viewType)
        {
            base.RegisterRelation(GetWrappedType(viewModelType, true), viewType);
        }

        public T Resolve<T>()
        {
            return (T)IoCManager.Container.Resolve(GetWrappedType<T>());
        }

        public void ResolveAndShow<T>() where T : ViewModelBase
        {
            ((T)IoCManager.Container.Resolve(GetWrappedType<T>())).Show();
        }
    }
}
