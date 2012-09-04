using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Interactivity;
using UseAbilities.WPF.Attributes;

namespace UseAbilities.WPF.Behaviors
{ 
    /// <summary>
    /// For using [DisplayName("Name")] and [Display("Name", Index)]
    /// </summary>
    public class ColumnHeaderBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            AssociatedObject.AutoGeneratingColumn += (sender, e) =>
            {
                var displayName = GetPropertyDisplayName(e.PropertyDescriptor);
                if (!string.IsNullOrEmpty(displayName)) e.Column.Header = displayName;
                else e.Cancel = true;

                var displayIndex = GetPropertyDisplayIndex(e.PropertyDescriptor);
                if (displayIndex < AssociatedObject.Columns.Count && displayIndex >= 0) e.Column.DisplayIndex = displayIndex;
            };
        }

        public virtual string GetPropertyDisplayName(object descriptor)
        {
            var pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                var attr = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if ((attr != null) && (!Equals(attr, DisplayNameAttribute.Default))) return attr.DisplayName;
            }
            else
            {
                var pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    var attrs = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    foreach (var att in attrs)
                    {
                        var attribute = att as DisplayNameAttribute;
                        if ((attribute != null) && (!Equals(attribute, DisplayNameAttribute.Default))) return attribute.DisplayName;
                    }
                }
            }

            return null;
        }

        public virtual int GetPropertyDisplayIndex(object descriptor)
        {
            var pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                var attr = pd.Attributes[typeof(Display)] as Display;
                if ((attr != null) && (!Equals(attr, DisplayNameAttribute.Default))) return attr.DisplayIndex;
            }
            else
            {
                var pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    var attrs = pi.GetCustomAttributes(typeof(Display), true);
                    foreach (var att in attrs)
                    {
                        var attribute = att as Display;
                        if ((attribute != null) && (!Equals(attribute, DisplayNameAttribute.Default))) return attribute.DisplayIndex;
                    }
                }
            }

            return -1;
        }
    }
}
