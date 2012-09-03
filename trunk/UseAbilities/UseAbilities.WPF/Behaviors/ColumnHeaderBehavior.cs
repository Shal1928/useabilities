﻿using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace UseAbilities.WPF.Behaviors
{ 
    /// <summary>
    /// For using [DisplayName("Name")]
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
    }
}
