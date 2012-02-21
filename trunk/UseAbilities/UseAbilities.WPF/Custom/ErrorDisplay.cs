using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Common.Code.UseAbilities.WPF.Custom
{
    public class ErrorDisplay : Control
    {
        public ErrorDisplay()
        {
            Visibility = Visibility.Collapsed;

            //DataTrigger.Binding=

            Style = new Style(typeof (ErrorDisplay));
            Style.Triggers.Add(DataTrigger);

            StackPanel.Orientation = Orientation.Vertical;
            StackPanel.Children.Add(TextBlock);

            AddVisualChild(StackPanel);
        }

        #region Control Content

        private StackPanel _stackPanel;
        private StackPanel StackPanel
        {
            get
            {
                return _stackPanel ?? (_stackPanel = new StackPanel());
            }
        }

        private TextBlock _textBlock;
        private TextBlock TextBlock
        {
            get
            {
                return _textBlock ?? (_textBlock = new TextBlock());
            }
        }

        private DataTrigger _dataTrigger;
        private DataTrigger DataTrigger
        {
            get
            {
                return _dataTrigger ?? (_dataTrigger = new DataTrigger());
            }
        }

        #endregion
        

    }
}
