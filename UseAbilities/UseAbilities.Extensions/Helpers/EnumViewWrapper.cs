using System;
using System.Collections.Generic;
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
        public Enum Value
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
                return Value.DescriptionOf();
            }
        }

        /// <summary>
        /// Constructor EnumViewWrapper
        /// </summary>
        /// <param name="value">Must be Enum</param>
        internal EnumViewWrapper(Enum value)
        {
            Value = value;
        }

        public static List<EnumViewWrapper<T>> GetWrappedCollection()
        {
            var resultCollection = new List<EnumViewWrapper<T>>();

            foreach (var enumValue in typeof(T).GetEnumValues())
                resultCollection.Add(new EnumViewWrapper<T>((Enum)enumValue));


            return resultCollection;
        }
    }
}
