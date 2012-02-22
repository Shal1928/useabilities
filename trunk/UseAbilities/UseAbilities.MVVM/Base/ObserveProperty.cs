using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace UseAbilities.MVVM.Base
{
    public abstract class ObserveProperty : IObservePropery
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged<TValue>(Expression<Func<TValue>> propertySelector)
        {
            if (PropertyChanged != null)
            {
                var memberExpression = propertySelector.Body as MemberExpression;

                if (memberExpression != null) PropertyChanged(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
