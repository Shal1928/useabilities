using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UseAbilities.WPF.AttachedProperties
{
    public static class VerifyBehavior
    {
        #region Verify

        public static readonly DependencyProperty VerifyProperty =
            DependencyProperty.RegisterAttached("Verify", typeof(bool?),
            typeof(VerifyBehavior), new UIPropertyMetadata(null, VerifyOnChanged));

        public static bool? GetVerify(Control control)
        {
            return (bool?)control.GetValue(VerifyProperty);
        }

        public static void SetVerify(Control control, bool? value)
        {
            control.SetValue(VerifyProperty, value);
        }

        #endregion

        #region Message

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.RegisterAttached("Message", typeof(string),
            typeof(VerifyBehavior), new UIPropertyMetadata(string.Empty, VerifyOnChanged));

        public static string GetMessage(Control control)
        {
            return (string)control.GetValue(MessageProperty);
        }

        public static void SetMessage(Control control, string value)
        {
            control.SetValue(MessageProperty, value);
        }

        #endregion
        

        private static void VerifyOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Control;

            if (control == null) return;
            var value = e.NewValue is bool? ? e.NewValue as bool? : control.GetValue(VerifyProperty) as bool?;
            var message = e.NewValue is string ? e.NewValue : control.GetValue(MessageProperty);
            if(value == null) return;

            var isShowWarning = !value.Value;

            if(isShowWarning)
            {
                control.ToolTip = new ToolTip
                                      {
                                          Content = message
                                      };
                control.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                control.ToolTip = null;
                control.Background = new SolidColorBrush(Colors.White);
            }
        }
    }
}
