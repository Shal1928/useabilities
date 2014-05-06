using System;
using System.Collections.Generic;
using System.Linq;
using UseAbilities.Extensions.EnumExt;
using UseAbilities.Extensions.Helpers.Interfaces;

namespace UseAbilities.Extensions.Helpers
{
    /// <summary>
    /// Wrapper for displaying description enum during the binding DescriptionOfExt
    /// </summary>
    /// <typeparam name="T">Must be Enum</typeparam>
    public class EnumViewWrapper<T> : IEnumViewValue
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
        public EnumViewWrapper(Enum value)
        {
            Value = value;
        }

        public static IEnumerable<EnumViewWrapper<T>> GetWrappedCollection()
        {
            return (from object enumValue in typeof (T).GetEnumValues() select new EnumViewWrapper<T>((Enum) enumValue)).ToList();
        }
    }

    public class EnumViewWrapper : EnumViewWrapper<Enum>
    {
        public EnumViewWrapper(Enum value): base(value)
        {
            //
        }
    }   
}
