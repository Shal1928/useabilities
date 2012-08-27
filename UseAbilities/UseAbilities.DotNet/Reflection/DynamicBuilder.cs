using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;

namespace UseAbilities.DotNet.Reflection
{
    public static class DynamicBuilder
    {
        public static void CreateInstance(string className)
        {
            var myDomain = Thread.GetDomain();
        }
    }

    public class PropertyBuilderDemo
    {
        public static Type BuildDynamicTypeWithProperties()
        {
            var myDomain = Thread.GetDomain();
            //var myAsmName = new AssemblyName
            //                    {
            //                        Name = "MyDynamicAssembly"
            //                    };

            var assemblyName = Assembly.GetAssembly(typeof (PropertyBuilderDemo)).GetName();
            
            // To generate a persistable assembly, specify AssemblyBuilderAccess.RunAndSave.
            var myAsmBuilder = myDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

            // Generate a persistable single-module assembly.
            var myModBuilder = myAsmBuilder.DefineDynamicModule(assemblyName.Name, assemblyName.Name + ".dll");

            var myTypeBuilder = myModBuilder.DefineType("CustomerData", TypeAttributes.Public);




            var customerNameBldr = myTypeBuilder.DefineField("customerName", typeof(string), FieldAttributes.Private);

            // The last argument of DefineProperty is null, because the
            // property has no parameters. (If you don't specify null, you must
            // specify an array of Type objects. For a parameterless property,
            // use an array with no elements: new Type[] {})
            var custNamePropBldr = myTypeBuilder.DefineProperty("CustomerName", PropertyAttributes.HasDefault, typeof(string), null);

            // The property set and property get methods require a special
            // set of attributes.
            const MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            // Define the "get" accessor method for CustomerName.
            var custNameGetPropMthdBldr = myTypeBuilder.DefineMethod("get_CustomerName", getSetAttr, typeof(string), Type.EmptyTypes);

            var custNameGetIL = custNameGetPropMthdBldr.GetILGenerator();

            custNameGetIL.Emit(OpCodes.Ldarg_0);
            custNameGetIL.Emit(OpCodes.Ldfld, customerNameBldr);
            custNameGetIL.Emit(OpCodes.Ret);

            // Define the "set" accessor method for CustomerName.
            var custNameSetPropMthdBldr = myTypeBuilder.DefineMethod("set_CustomerName", getSetAttr, null, new[] { typeof(string) });

            var custNameSetIL = custNameSetPropMthdBldr.GetILGenerator();

            custNameSetIL.Emit(OpCodes.Ldarg_0);
            custNameSetIL.Emit(OpCodes.Ldarg_1);
            custNameSetIL.Emit(OpCodes.Stfld, customerNameBldr);
            custNameSetIL.Emit(OpCodes.Ret);

            // Last, we must map the two methods created above to our PropertyBuilder to 
            // their corresponding behaviors, "get" and "set" respectively. 
            custNamePropBldr.SetGetMethod(custNameGetPropMthdBldr);
            custNamePropBldr.SetSetMethod(custNameSetPropMthdBldr);


            var retval = myTypeBuilder.CreateType();

            // Save the assembly so it can be examined with Ildasm.exe,
            // or referenced by a test program.
            myAsmBuilder.Save(assemblyName.Name + ".dll");
            return retval;
        }

        //public static void Main()
        //{
        //    var custDataType = BuildDynamicTypeWithProperties();

        //    PropertyInfo[] custDataPropInfo = custDataType.GetProperties();
        //    foreach (PropertyInfo pInfo in custDataPropInfo)
        //    {
        //        Console.WriteLine("Property '{0}' created!", pInfo.ToString());
        //    }

        //    Console.WriteLine("---");
        //    // Note that when invoking a property, you need to use the proper BindingFlags -
        //    // BindingFlags.SetProperty when you invoke the "set" behavior, and 
        //    // BindingFlags.GetProperty when you invoke the "get" behavior. Also note that
        //    // we invoke them based on the name we gave the property, as expected, and not
        //    // the name of the methods we bound to the specific property behaviors.

        //    object custData = Activator.CreateInstance(custDataType);
        //    custDataType.InvokeMember("CustomerName", BindingFlags.SetProperty,
        //                                  null, custData, new object[] { "Joe User" });

        //    Console.WriteLine("The customerName field of instance custData has been set to '{0}'.",
        //                       custDataType.InvokeMember("CustomerName", BindingFlags.GetProperty,
        //                                                  null, custData, new object[] { }));
        }
}
