using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using UseAbilities.MVVM.Command;
using Control = System.Windows.Controls.Control;

namespace UseAbilities.Controls.Dialogs
{
    [DesignTimeVisible(false)]
    public class DependencyFolderBrowseDialog : Control
    {
        public DependencyFolderBrowseDialog()
        {
            Visibility = Visibility.Collapsed;
        }

        private FolderBrowserDialog _dialog;
        private FolderBrowserDialog Dialog
        {
            get
            {
                return _dialog ?? (_dialog = new FolderBrowserDialog());
            }
        }
        

        #region Commands

        public static readonly DependencyProperty FolderOkCommandProperty =
            DependencyProperty.Register("FolderOkCommand", typeof(ICommand), typeof(DependencyFolderBrowseDialog));

        public ICommand FolderOkCommand
        {
            get
            {
                return (ICommand)GetValue(FolderOkCommandProperty);
            }
            set
            {
                SetValue(FolderOkCommandProperty, value);
            }
        }


        private ICommand _showDialogCommand;
        public ICommand ShowDialogCommand
        {
            get
            {
                return _showDialogCommand ?? (_showDialogCommand = new RelayCommand(p => ShowDialog()));
            }
        }

        private void ShowDialog()
        {
            if(Dialog.ShowDialog()!=DialogResult.OK) return;

            if (FolderOkCommand != null) FolderOkCommand.Execute(Dialog.SelectedPath);
        }

        #endregion


        #region Dependency Properties

        #region Description

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(DependencyFolderBrowseDialog),
            new UIPropertyMetadata("Select a folder", DialogPropertyChangedCallback));

        public string Description
        {
            get
            {
                return (string)GetValue(DescriptionProperty);
            }
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }

        #endregion

        #region ShowNewFolderButton

        public static readonly DependencyProperty ShowNewFolderButtonProperty =
            DependencyProperty.Register("ShowNewFolderButton", typeof(bool), typeof(DependencyFolderBrowseDialog),
            new UIPropertyMetadata(true, DialogPropertyChangedCallback));

        public bool ShowNewFolderButton
        {
            get
            {
                return (bool)GetValue(ShowNewFolderButtonProperty);
            }
            set
            {
                SetValue(ShowNewFolderButtonProperty, value);
            }
        }

        #endregion

        #region RootFolder

        public static readonly DependencyProperty RootFolderProperty =
            DependencyProperty.Register("SelectedPath", typeof(Environment.SpecialFolder), typeof(DependencyFolderBrowseDialog),
            new UIPropertyMetadata(Environment.SpecialFolder.Desktop, DialogPropertyChangedCallback));

        public Environment.SpecialFolder RootFolder
        {
            get
            {
                return (Environment.SpecialFolder)GetValue(RootFolderProperty);
            }
            set
            {
                SetValue(RootFolderProperty, value);
            }
        }

        #endregion

        private static void DialogPropertyChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var dfd = obj as DependencyFolderBrowseDialog;
            if (dfd == null) return;

            var changedProp = dfd.Dialog.GetType().GetProperty(args.Property.Name);
            if (changedProp.CanWrite) changedProp.SetValue(dfd.Dialog, args.NewValue, null);
        }

        #endregion
    }
}
