using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace UseAbilities.Visual.Controls.FilteringDataGridDictionary
{
    /// <summary>
    /// A grid that makes inline filtering possible.
    /// </summary>
    public class FilteringDataGrid : DataGrid
    {
        /// <summary>
        /// This dictionary will have a list of all applied filters
        /// </summary>
        private readonly Dictionary<string, string> _columnFilters;

        /// <summary>
        /// Cache with properties for better performance
        /// </summary>
        private readonly Dictionary<string, PropertyInfo> _propertyCache;

        /// <summary>
        /// Case sensitive filtering
        /// </summary>
        public static DependencyProperty IsFilteringCaseSensitiveProperty =
             DependencyProperty.Register("IsFilteringCaseSensitive",
             typeof(bool),
             typeof(FilteringDataGrid),
             new PropertyMetadata(true));

        /// <summary>
        /// Case sensitive filtering
        /// </summary>
        public bool IsFilteringCaseSensitive
        {
            get
            {
                return (bool)(GetValue(IsFilteringCaseSensitiveProperty));
            }
            set
            {
                SetValue(IsFilteringCaseSensitiveProperty, value);
            }
        }

        /// <summary>
        /// Register for all text changed events
        /// </summary>
        public FilteringDataGrid()
        {
            // Initialize lists
            _columnFilters = new Dictionary<string, string>();
            _propertyCache = new Dictionary<string, PropertyInfo>();

            // Add a handler for all text changes
            AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(OnTextChanged), true);

            // Datacontext changed, so clear the cache
            DataContextChanged += FilteringDataGridDataContextChanged;
        }

        /// <summary>
        /// Clear the property cache if the datacontext changes.
        /// This could indicate that an other type of object is bound.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilteringDataGridDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _propertyCache.Clear();
        }

        /// <summary>
        /// When a text changes, it might be required to filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Get the textbox
            var filterTextBox = e.OriginalSource as TextBox;

            // Get the header of the textbox
            var header = TryFindParent<DataGridColumnHeader>(filterTextBox);
            if (header == null) return;

            UpdateFilter(filterTextBox, header);
            ApplyFilters();
        }

        /// <summary>
        /// Update the internal filter
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="header"></param>
        private void UpdateFilter(TextBox textBox, DataGridColumnHeader header)
        {
            // Try to get the property bound to the column.
            // This should be stored as datacontext.
            var columnBinding = header.DataContext != null ? header.DataContext.ToString() : "";

            // Set the filter 
            if (!String.IsNullOrEmpty(columnBinding)) _columnFilters[columnBinding] = textBox.Text;
        }

        /// <summary>
        /// Apply the filters
        /// </summary>
        private void ApplyFilters()
        {
            // Get the view
            var view = CollectionViewSource.GetDefaultView(ItemsSource);
            if (view != null)
            {
                // Create a filter
                view.Filter = delegate(object item)
                {
                    // Show the current object
                    var show = true;

                    // Loop filters
                    foreach (var filter in _columnFilters)
                    {
                        var property = GetPropertyValue(item, filter.Key);
                        if (property == null) continue;

                        // Check if the current column contains a filter
                        var containsFilter = IsFilteringCaseSensitive ?
                                             property.ToString().Contains(filter.Value) :
                                             property.ToString().ToLower().Contains(filter.Value.ToLower());

                        // Do the necessary things if the filter is not correct
                        if (containsFilter) continue;

                        show = false;
                        break;
                    }

                    // Return if it's visible or not
                    return show;
                };
            }
        }

        /// <summary>
        /// Get the value of a property
        /// </summary>
        /// <param name="item"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private object GetPropertyValue(object item, string property)
        {
            // No value
            object value = null;

            // Get property  from cache
            PropertyInfo pi;
            if (_propertyCache.ContainsKey(property)) pi = _propertyCache[property];
            else
            {
                pi = item.GetType().GetProperty(property);
                _propertyCache.Add(property, pi);
            }

            // If we have a valid property, get the value
            if (pi != null) value = pi.GetValue(item, null);

            // Done
            return value;
        }

        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null reference is being returned.</returns>
        public static T TryFindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            var parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            var parent = parentObject as T;
            return parent ?? TryFindParent<T>(parentObject);

            //use recursion to proceed with next level
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Do note, that for content element,
        /// this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise null.</returns>
        public static DependencyObject GetParentObject(DependencyObject child)
        {
            if (child == null) return null;
            var contentElement = child as ContentElement;

            if (contentElement != null)
            {
                var parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                var fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            // If it's not a ContentElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }
    }
}
