using System.ComponentModel;
using System.Reflection;
using System.Windows;
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
        #region DependencyProperty CellTemplate

        public static readonly DependencyProperty CellTemplateProperty =
            DependencyProperty.Register("CellTemplate",
                                        typeof(DataTemplate),
                                        typeof(ColumnHeaderBehavior),
                                        new UIPropertyMetadata(null, CellTemplateChanged)
                                        );

        public DataTemplate CellTemplate
        {
            get
            {
                return (DataTemplate)GetValue(CellTemplateProperty);
            }
            set
            {
                SetValue(CellTemplateProperty, value);
            }
        }

        private static void CellTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion

        #region DependencyProperty CellEditingTemplate

        public static readonly DependencyProperty CellEditingTemplateProperty =
            DependencyProperty.Register("CellEditingTemplate",
                                        typeof(DataTemplate),
                                        typeof(ColumnHeaderBehavior),
                                        new UIPropertyMetadata(null, CellEditingTemplateChanged)
                                        );

        public DataTemplate CellEditingTemplate
        {
            get
            {
                return (DataTemplate)GetValue(CellEditingTemplateProperty);
            }
            set
            {
                SetValue(CellEditingTemplateProperty, value);
            }
        }

        private static void CellEditingTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion

        protected override void OnAttached()
        {
            AssociatedObject.AutoGeneratingColumn += (sender, e) =>
            {
                var displayName = GetPropertyDisplayName(e.PropertyDescriptor);
                if (!string.IsNullOrEmpty(displayName))
                {
                    //var binding = ((DataGridBoundColumn)e.Column).Binding;                 

                    //var dataGridTemplateColumn = new DataGridTemplateColumn
                    //                {
                    //                    CellTemplate = CellTemplate,
                    //                    CellEditingTemplate = CellEditingTemplate,
                    //                    Header = displayName
                    //                };

                    //e.Column = dataGridTemplateColumn;

                    e.Column.Header = displayName;
                }
                else e.Cancel = true;

                var displayIndex = GetPropertyDisplayIndex(e.PropertyDescriptor);
                if (displayIndex >= AssociatedObject.Columns.Count) return;

                if (displayIndex >= 0) e.Column.DisplayIndex = displayIndex;
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
