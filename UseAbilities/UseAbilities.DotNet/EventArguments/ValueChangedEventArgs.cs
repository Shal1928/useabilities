using System;

namespace UseAbilities.DotNet.EventArguments
{
    public class ValueChangedEventArgs : EventArgs
    {
        public ValueChangedEventArgs(object oldValue, object newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public readonly object OldValue;
        public readonly object NewValue;
    }
}
