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
        public Display(string displayName, int displayIndex, string format = null)
        {
            Initialize(displayName, displayIndex, DataGridLength.SizeToHeader, format);
        }

        public Display(string displayName, int displayIndex, int widthStars, string format = null)
        {
            Initialize(displayName, displayIndex, new DataGridLength(widthStars, DataGridLengthUnitType.Star), format);
        }

        public Display(string displayName, int displayIndex, DisplayColumnType displayColumnType, string format = null)
        {
            DataGridLength width;
            switch (displayColumnType)
            {
                case DisplayColumnType.Auto: width = DataGridLength.Auto; break;
                case DisplayColumnType.SizeToCells: width = DataGridLength.SizeToCells; break;
                default: width = DataGridLength.SizeToHeader; break;
            }

            Initialize(displayName, displayIndex, width, format);
        }

        public Display(string displayName, int displayIndex, double width, string format = null)
        {
            Initialize(displayName, displayIndex, new DataGridLength(width), format);
        }

        public Display(string displayName, int displayIndex, DataGridLength width, string format = null)
        {
            Initialize(displayName, displayIndex, width, format);
        }

        private void Initialize(string displayName, int displayIndex, DataGridLength width, string format)
        {
            DisplayNameValue = displayName;
            DisplayIndex = displayIndex;
            Width = width;
            Format = format;
        }

        public int DisplayIndex { get; protected set; }
        public DataGridLength Width { get; protected set;}
        public string Format { get; protected set; }

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

        public void ChangeFormat(string format)
        {
            Format = format;
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
