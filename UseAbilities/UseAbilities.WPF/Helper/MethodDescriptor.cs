using System;
using System.Reflection;

namespace UseAbilities.WPF.Helper
{
    public class MethodDescriptor
    {
        public MethodDescriptor(MethodInfo methodInfo, ParameterInfo[] methodParams)
        {
            MethodInfo = methodInfo;
            Parameters = methodParams;
        }

        public bool HasParameters
        {
            get
            {
                return (Parameters.Length > 0);
            }
        }

        public MethodInfo MethodInfo { get; private set; }

        public int ParameterCount
        {
            get
            {
                return Parameters.Length;
            }
        }

        public ParameterInfo[] Parameters { get; private set; }

        public Type SecondParameterType
        {
            get
            {
                return Parameters.Length >= 2 ? Parameters[1].ParameterType : null;
            }
        }
    }
}
