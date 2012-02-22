using Microsoft.Win32;
using System.Windows;
using UseAbilities.Controls.Dialogs.Base;

namespace UseAbilities.Controls.Dialogs
{
    public class DependencySaveFileDialog : DependencyFileDialog
    {
        private SaveFileDialog _dialog;

        /// <summary>
        /// Overridden from DependencyFileDialog. Provides access to an instance of the Microsoft.Win32.SaveFileDialog class.
        /// </summary>
        protected override FileDialog Dialog
        {
            get
            {
                return _dialog ?? (_dialog = new SaveFileDialog());
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether DependencySaveFileDialog prompts the user for permission 
        /// to create a file if the user specifies a file that does not exist. 
        /// </summary>
        public bool CreatePrompt
        {
            get
            {
                return (bool)GetValue(CreatePromptProperty);
            }
            set
            {
                SetValue(CreatePromptProperty, value);
            }
        }

        /// <summary>
        /// The Dependency Property for the CreatePrompt property
        /// </summary>
        public static readonly DependencyProperty CreatePromptProperty =
            DependencyProperty.Register("CreatePrompt", typeof(bool), typeof(DependencySaveFileDialog),
            new UIPropertyMetadata(false, DialogPropertyChangedCallback));

        /// <summary>
        /// Gets or sets a value indicating whether SaveFileDialog displays a warning if the user specifies the name of a file that already exists. 
        /// </summary>
        public bool OverwritePrompt
        {
            get
            {
                return (bool)GetValue(OverwritePromptProperty);
            }
            set
            {
                SetValue(OverwritePromptProperty, value);
            }
        }

        /// <summary>
        /// The Dependency Property for the OverwritePrompt property
        /// </summary>
        public static readonly DependencyProperty OverwritePromptProperty =
            DependencyProperty.Register("OverwritePrompt", typeof(bool), typeof(DependencySaveFileDialog),
            new UIPropertyMetadata(true, DialogPropertyChangedCallback));
    }
}