using System.Windows;
using System.Windows.Input;

namespace UseAbilities.WPF.AttachedBehaviors
{
    //<Trigger Property="TriggerTest:IsPressedBehavior.IsPressed" Value="True"> 
    //                <Setter Property="Foreground" Value="Green" /> 
    //</Trigger>

    public static class IsMousePressedAttachedBehavior
    {
        public static bool GetMonitorMouse(DependencyObject obj)
        {
            return (bool)obj.GetValue(MonitorMouseProperty);
        }

        public static void SetMonitorMouse(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPressedProperty, value);
        }

        public static readonly DependencyProperty MonitorMouseProperty =
            DependencyProperty.RegisterAttached("MonitorMouse",
                                                typeof(bool),
                                                typeof(IsMousePressedAttachedBehavior),
                                                new UIPropertyMetadata(false, OnMonitorMouse));

        public static bool GetIsPressed(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPressedProperty);
        }

        public static void SetIsPressed(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPressedProperty, value);
        }

        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.RegisterAttached("IsPressed",
                                                typeof(bool),
                                                typeof(IsMousePressedAttachedBehavior),
                                                new UIPropertyMetadata(false));

        private static void OnMonitorMouse(DependencyObject depObj, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var uiElement = depObj as UIElement;
            if (uiElement == null)
            {
                return;
            }
            if ((bool)dependencyPropertyChangedEventArgs.NewValue)
            {
                uiElement.MouseDown += OnMouseDown;
                uiElement.MouseUp += OnMouseUp;
                uiElement.MouseLeave += OnMouseLeave;
                uiElement.MouseEnter += OnMouseEnter;
            }
            else
            {
                uiElement.MouseDown -= OnMouseDown;
                uiElement.MouseUp -= OnMouseUp;
                uiElement.MouseLeave -= OnMouseLeave;
                uiElement.MouseEnter -= OnMouseEnter;
            }
        }

        private static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SetIsPressed(sender as DependencyObject, true);
        }

        private static void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            SetIsPressed(sender as DependencyObject, false);
        }

        private static void OnMouseLeave(object sender, MouseEventArgs e)
        {
            SetIsPressed(sender as DependencyObject, false);
        }

        static void OnMouseEnter(object sender, MouseEventArgs e)
        {
            SetIsPressed(sender as DependencyObject, e.LeftButton == MouseButtonState.Pressed || e.MiddleButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed);
        }            
    }


}
