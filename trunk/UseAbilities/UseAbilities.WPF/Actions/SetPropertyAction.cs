using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interactivity;

namespace UseAbilities.WPF.Actions
{
    public class SetPropertyAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var descriptor = DependencyPropertyDescriptor.FromName(Property, AssociatedObject.GetType(), AssociatedObject.GetType());

            var targetType = descriptor.DependencyProperty.PropertyType;

            if (targetType.IsEnum) descriptor.SetValue(AssociatedObject, Enum.Parse(targetType, Value.ToString()));
            else descriptor.SetValue(AssociatedObject, Value);
        }

        #region

        public static readonly DependencyProperty PropertyProperty =
            DependencyProperty.Register("Property",
                                        typeof(string),
                                        typeof(SetPropertyAction),
                                        new UIPropertyMetadata(null, OnPropertyChanged)
                                        );

        public string Property
        {
            get
            {
                return (string)GetValue(PropertyProperty);
            }
            set
            {
                SetValue(PropertyProperty, value);
            }
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Value

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value",
                                        typeof(object),
                                        typeof(SetPropertyAction),
                                        new UIPropertyMetadata(null, OnValueChanged)
                                        );

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

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion
        
    }
}
