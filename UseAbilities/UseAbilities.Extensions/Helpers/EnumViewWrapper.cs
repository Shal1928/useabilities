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
        public EnumViewWrapper(Enum value)
        {
            Value = value;
        }

        //public EnumViewWrapper(Type enumType)
        //{
        //    Value = enumType.GetEnumValues();
        //}

        public static List<EnumViewWrapper<T>> GetWrappedCollection()
        {
            var resultCollection = new List<EnumViewWrapper<T>>();

            foreach (var enumValue in typeof(T).GetEnumValues())
                resultCollection.Add(new EnumViewWrapper<T>((Enum)enumValue));


            return resultCollection;
        }
    }

    public class EnumViewWrapper : EnumViewWrapper<Enum>
    {
        public EnumViewWrapper(Enum value): base(value)
        {
            //
        }
    }

    //public static class EnumViewWrapperExtensions
    //{
    //    public static Enum GetEnumValue(this Enum enumWrapper, Type enumType)
    //    {

    //    }
    //}

    
}
