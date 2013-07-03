using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Interactivity;
using UseAbilities.WPF.Helper;

namespace UseAbilities.WPF.Actions
{
    public class CallMethodAction : TargetedTriggerAction<FrameworkElement>
    {
        private List<MethodDescriptor> _methodDescriptors = new List<MethodDescriptor>();

        protected override void OnAttached()
        {
            base.OnAttached();
            UpdateMethodInfo();
        }

        protected override void OnDetaching()
        {
            _methodDescriptors.Clear();
            base.OnDetaching();
        }

        protected override void Invoke(object parameter)
        {
            if (AssociatedObject == null) return;

            var descriptor = FindBestMethod(parameter);
            if (descriptor != null)
            {
                var parameters = descriptor.Parameters;
                if (parameters.Length == 0) descriptor.MethodInfo.Invoke(Target, null);
                else if ((((parameters.Length == 2) &&
                           (AssociatedObject != null)) &&
                          ((parameter != null) &&
                           parameters[0].ParameterType.IsAssignableFrom(AssociatedObject.GetType()))) &&
                         parameters[1].ParameterType.IsAssignableFrom(parameter.GetType()))
                    descriptor.MethodInfo.Invoke(Target, new[] { AssociatedObject, parameter });
            }
            else if (TargetObject != null)
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "No valid method found.",
                                                          new object[] { MethodName, TargetObject.GetType().Name }));
        }

        public void InvokeInternal()
        {
            if (AssociatedObject == null) return;
            foreach (var info in AssociatedObject.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(IsMethodValid))
                info.Invoke(AssociatedObject, new object[0]);
        }

        #region MethodNameProperty

        public static readonly DependencyProperty MethodNameProperty = DependencyProperty.Register(
           "MethodName",
           typeof(string),
           typeof(CallMethodAction),
           new PropertyMetadata(OnMethodNameChanged));

        public string MethodName
        {
            get
            {
                return (string)GetValue(MethodNameProperty);
            }
            set
            {
                SetValue(MethodNameProperty, value);
            }
        }

        private static void OnMethodNameChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((CallMethodAction)sender).UpdateMethodInfo();
        }

        #endregion


        #region TargetObjectProperty

        public new static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(
            "TargetObject",
            typeof(object),
            typeof(CallMethodAction),
            new PropertyMetadata(OnTargetObjectChanged));

        public new object TargetObject
        {
            get
            {
                return GetValue(TargetObjectProperty);
            }
            set
            {
                SetValue(TargetObjectProperty, value);
            }
        }

        private static void OnTargetObjectChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((CallMethodAction)sender).UpdateMethodInfo();
        }

        #endregion


        #region Private Methods

        private static bool AreMethodParamsValid(ParameterInfo[] methodParams)
        {
            if (methodParams.Length == 2)
            {
                if (methodParams[0].ParameterType != typeof(object)) return false;
                if (!typeof(EventArgs).IsAssignableFrom(methodParams[1].ParameterType)) return false;
            }
            else if (methodParams.Length != 0) return false;

            return true;
        }

        private MethodDescriptor FindBestMethod(object parameter)
        {
            if (parameter != null) parameter.GetType();

            return _methodDescriptors.FirstOrDefault(methodDescriptor => (!methodDescriptor.HasParameters || ((parameter != null) && methodDescriptor.SecondParameterType.IsAssignableFrom(parameter.GetType()))));
        }

        private void UpdateMethodInfo()
        {
            _methodDescriptors.Clear();
            if ((Target == null) || string.IsNullOrEmpty(this.MethodName)) return;

            foreach (var info in Target.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!IsMethodValid(info)) continue;

                var parameters = info.GetParameters();
                if (AreMethodParamsValid(parameters)) _methodDescriptors.Add(new MethodDescriptor(info, parameters));
            }

            _methodDescriptors = _methodDescriptors.OrderByDescending(delegate(MethodDescriptor methodDescriptor)
            {
                var num = 0;
                if (methodDescriptor.HasParameters)
                    for (var type = methodDescriptor.SecondParameterType; type != typeof(EventArgs); type = type.BaseType)
                        num++;

                return (methodDescriptor.ParameterCount + num);
            }).ToList();
        }


        private bool IsMethodValid(MethodInfo method)
        {
            if (!string.Equals(method.Name, this.MethodName, StringComparison.Ordinal)) return false;

            return method.ReturnType == typeof(void);
        }

        private new object Target
        {
            get
            {
                return (TargetObject ?? AssociatedObject);
            }
        }

        #endregion
    }
}
