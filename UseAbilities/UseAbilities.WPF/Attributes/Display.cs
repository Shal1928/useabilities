using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace UseAbilities.WPF.Attributes
{
    /// <summary>
    /// Extended DisplayName Attribute (add DisplayIndex)
    /// </summary>
    public class Display : DisplayNameAttribute
    {
        public Display(string displayName, int displayIndex)
        {
            Initialize(displayName, displayIndex, DataGridLength.SizeToHeader);
        }

        public Display(string displayName, int displayIndex, int widthStars)
        {
            Initialize(displayName, displayIndex, new DataGridLength(widthStars, DataGridLengthUnitType.Star));
        }

        public Display(string displayName, int displayIndex, DisplayColumnType displayColumnType)
        {
            DataGridLength width;
            switch (displayColumnType)
            {
                case DisplayColumnType.Auto: width = DataGridLength.Auto; break;
                case DisplayColumnType.SizeToCells: width = DataGridLength.SizeToCells; break;
                default: width = DataGridLength.SizeToHeader; break;
            }

            Initialize(displayName, displayIndex, width);
        }

        public Display(string displayName, int displayIndex, double width)
        {
            Initialize(displayName, displayIndex, new DataGridLength(width));
        }

        public Display(string displayName, int displayIndex, DataGridLength width)
        {
            Initialize(displayName, displayIndex, width);
        }

        private void Initialize(string displayName, int displayIndex, DataGridLength width)
        {
            DisplayNameValue = displayName;
            DisplayIndex = displayIndex;
            Width = width;
        }

        public int DisplayIndex
        {
            get; 
            protected set; 
        }

        public DataGridLength Width
        {
            get;
            protected set;
        }

        public void ChangeDisplayName(string displayName, params object[] args)
        {
            DisplayNameValue = string.Format(displayName, args ?? new object[0]);
        }

        public void ChangeDisplayIndex(int displayIndex)
        {
            DisplayIndex = displayIndex;
        }

        public void ChangeWidth(DataGridLength width)
        {
            Width = width;
        }

        public void Hide()
        {
            DisplayIndex = -1;
        }
    }

    public enum DisplayColumnType
    {
        Auto,
        SizeToCells,
        SizeToHeader
    }
}
