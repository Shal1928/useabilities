using System;
using System.Diagnostics;
using UseAbilities.Extensions.EnumExt;

namespace UseAbilities.Extensions.Helpers
{
    /// <summary>
    /// Wrapper for displaying description enum during the binding DescriptionOfExt
    /// </summary>
    /// <typeparam name="T">Must be Enum</typeparam>
    public class EnumViewWrapper<T>
    {
        /// <summary>
        /// Enum Value
        /// </summary>
        public T Value
        {
            get; 
            private set;
        }

        /// <summary>
        /// DescriptionOf Enum Value
        /// </summary>
        public string View
        {
            get
            {
                var value = (object)Value;
                Debug.Assert(value is Enum, "T is not Enum");
                return ((Enum)value).DescriptionOf();
            }
        }

        /// <summary>
        /// Constructor EnumViewWrapper
        /// </summary>
        /// <param name="tValue">Must be Enum</param>
        public EnumViewWrapper(T tValue)
        {
            Debug.Assert(tValue is Enum, "T is not Enum");
            Value = tValue;
        }
    }
}
