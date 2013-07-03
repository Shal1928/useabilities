using System;
using System.Windows;
using System.Windows.Interactivity;

namespace UseAbilities.WPF.Triggers
{
    public class PropertyChangedTrigger : TriggerBase<DependencyObject>
    {
        #region BindingProperty

        public static readonly DependencyProperty BindingProperty = DependencyProperty.Register(
            "Binding",
            typeof(object),
            typeof(PropertyChangedTrigger),
            new PropertyMetadata(OnBindingChanged));

        public object Binding
        {
            get
            {
                return GetValue(BindingProperty);
            }
            set
            {
                SetValue(BindingProperty, value);
            }
        }

        private static void OnBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((PropertyChangedTrigger)sender).EvaluateBindingChange(args);
        }

        #endregion


        #region ValueProperty

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(object),
            typeof(PropertyChangedTrigger),
            new PropertyMetadata(null));



        public object Value
        {
            get
            {
                return GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        protected virtual void EvaluateBindingChange(object args)
        {
            var propertyChangedArgs = (DependencyPropertyChangedEventArgs)args;
            var newValue = propertyChangedArgs.NewValue.ToString();
            var equal = string.Equals(newValue, Value.ToString(), StringComparison.InvariantCultureIgnoreCase);
            if (equal) InvokeActions(args);
        }

        #endregion

        

        
    }
}
