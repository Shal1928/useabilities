using System;

namespace UseAbilities.DotNet.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class TypeBinding : Attribute
    {
        public TypeBinding(Type type)
        {
            BindingWith = type;
        }

        public Type BindingWith
        {
            get;
            private set;
        }
    }
}
