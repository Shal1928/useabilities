﻿using System;
using System.Diagnostics;
using Common.Code.UseAbilities.MVVM.Managers;
using Common.MVVM.Core.Base;

namespace Common.Code.UseAbilities.MVVM.Base
{
    public abstract class ViewModelBase : ObserveProperty, IDisposable
    {
        private bool _isClosed = true;

        public virtual void Show()
        {
            _isClosed = false;
            ViewModelManager.RegisterViewModel(this);
        }

        public virtual void Close()
        {
            if (_isClosed) return;

            _isClosed = true;
            ViewModelManager.RemoveViewModel(this);
        }

        private bool _isActive = true;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnPropertyChanged(() => IsActive);
            }
        }

        public bool IsModal { get; set; }

        public virtual string DisplayName
        {
            get;
            protected set;
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {

        }

#if DEBUG
        /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            var msg = string.Format("{0} ({1}) ({2}) Finalized", GetType().Name, DisplayName, GetHashCode());
            Debug.WriteLine(msg);
        }
#endif
    }
}
