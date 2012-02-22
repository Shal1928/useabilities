using Microsoft.Win32;
using System.Windows;
using UseAbilities.Controls.Dialogs.Base;

namespace UseAbilities.Controls.Dialogs
{
    public class DependencyOpenFileDialog : DependencyFileDialog
    {
        private OpenFileDialog _dialog;

        /// <summary>
        /// Overridden from DependencyFileDialog. Provides access to an instance of the Microsoft.Win32.OpenFileDialog class.
        /// </summary>
        protected override FileDialog Dialog
        {
            get
            {
                return _dialog ?? (_dialog = new OpenFileDialog());
            }
        }

        /// <summary>
        /// Overridden from DependencyFileDialog. Provides support for the MultiSelect property. Smells fragile.
        /// </summary>
        protected override void ExecuteFileOkCommand()
        {
            if (MultiSelect)
            {
                FileOkCommand.Execute(Dialog.FileNames);
            }
            else
            {
                base.ExecuteFileOkCommand();
            }
        }

        /// <summary>
        /// Gets or sets an option indicating whether OpenFileDialog allows users to select multiple files.
        /// </summary>
        public bool MultiSelect
        {
            get
            {
                return (bool)GetValue(MultiSelectProperty);
            }
            set
            {
                SetValue(MultiSelectProperty, value);
            }
        }

        /// <summary>
        /// The Dependency Property for the MultiSelect property
        /// </summary>
        public static readonly DependencyProperty MultiSelectProperty =
            DependencyProperty.Register("MultiSelect", typeof(bool), typeof(DependencyOpenFileDialog),
            new UIPropertyMetadata(false, DialogPropertyChangedCallback));

        /// <summary>
        /// Gets or sets a value indicating whether the read-only check box displayed by DependencyOpenFileDialog is selected. 
        /// </summary>
        public bool ReadOnlyChecked
        {
            get
            {
                return (bool)GetValue(ReadOnlyCheckedProperty);
            }
            set
            {
                SetValue(ReadOnlyCheckedProperty, value);
            }
        }

        /// <summary>
        /// The Dependency Property for the ReadOnlyChecked property
        /// </summary>
        public static readonly DependencyProperty ReadOnlyCheckedProperty =
            DependencyProperty.Register("ReadOnlyChecked", typeof(bool), typeof(DependencyOpenFileDialog),
            new UIPropertyMetadata(false, DialogPropertyChangedCallback));

        /// <summary>
        /// Gets or sets a value indicating whether DependencyOpenFileDialog contains a read-only check box.
        /// </summary>
        public bool ShowReadOnly
        {
            get
            {
                return (bool)GetValue(ShowReadOnlyProperty);
            }
            set
            {
                SetValue(ShowReadOnlyProperty, value);
            }
        }

        /// <summary>
        /// The Dependency Property for the ShowReadOnly property
        /// </summary>
        public static readonly DependencyProperty ShowReadOnlyProperty =
            DependencyProperty.Register("ShowReadOnly", typeof(bool), typeof(DependencyOpenFileDialog),
            new UIPropertyMetadata(false, DialogPropertyChangedCallback));
    }
}
