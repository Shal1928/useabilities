using System.ComponentModel;

namespace UseAbilities.WPF.Attributes
{
    /// <summary>
    /// Extended DisplayName Attribute (add DisplayIndex)
    /// </summary>
    public class Display : DisplayNameAttribute
    {
        public Display(string displayName, int displayIndex)
        {
            DisplayNameValue = displayName;
            DisplayIndex = displayIndex;
        }

        public int DisplayIndex
        {
            get; 
            protected set; 
        }
    }
}
