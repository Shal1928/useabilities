using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Interactivity;
using UseAbilities.Extensions.ObjectExt;
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
                if (e.PropertyType == typeof(DateTime))
                {
                    var dataGridTextColumn = e.Column as DataGridTextColumn;
                    if (dataGridTextColumn != null)
                    {
                        dataGridTextColumn.Binding.StringFormat = GetAttributeProperty<string>(e.PropertyDescriptor, "Format");
                    }
                }

                var displayName = GetAttributeProperty<string>(e.PropertyDescriptor, "DisplayName");
                if (!string.IsNullOrEmpty(displayName)) e.Column.Header = displayName;
                else e.Cancel = true;

                var displayIndex = GetAttributeProperty<int>(e.PropertyDescriptor, "DisplayIndex", -1);

                if (displayIndex < AssociatedObject.Columns.Count)
                    if (displayIndex >= 0) e.Column.DisplayIndex = displayIndex;
                    else e.Cancel = true;

                var width = GetAttributeProperty<DataGridLength>(e.PropertyDescriptor, "Width", DataGridLength.SizeToHeader);
                e.Column.Width = width;
            };
        }

        protected virtual T GetAttributeProperty<T>(object descriptor, string propertyName, object defaultValue = null)
        {
            var pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                var attr = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if ((attr != null) && (!Equals(attr, DisplayNameAttribute.Default))) return attr.GetValue<T>(propertyName);
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
                        if ((attribute != null) && (!Equals(attribute, DisplayNameAttribute.Default))) return attribute.GetValue<T>(propertyName);
                    }
                }
            }

            return (T) defaultValue;
        }
    }
}
