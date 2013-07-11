using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace UseAbilities.MVVM.Base
{
    /// <summary>
    /// Not tested with complicated get methods
    /// </summary>
    public static class ObserveWrapper
    {
        #region Private Fields

        private static AssemblyBuilder _assemblyBuilder;
        private static string _moduleName;
        //private static string _assemblyFileName;

        #endregion

        
        public static Type Wrap(Type observePropertyType)
        {
            var propertyInfoCollection = observePropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            const MethodAttributes getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual;
            var typeBuilder = GetSeedClassBuilder(observePropertyType);           

            foreach (var propertyInfo in propertyInfoCollection.Where(pi => pi.GetSetMethod() != null && pi.GetSetMethod().IsVirtual))
            {
                //var valueField = typeBuilder.DefineField("_" + propertyInfo.Name, propertyInfo.PropertyType, FieldAttributes.Private);

                var property = typeBuilder.DefineProperty(propertyInfo.Name, propertyInfo.Attributes, propertyInfo.PropertyType, null);

                //get
                var valuePropertyGet = typeBuilder.DefineMethod("get_" + propertyInfo.Name, getSetAttributes, propertyInfo.PropertyType, null);

                var il = valuePropertyGet.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                //il.Emit(OpCodes.Ldfld, valueField); //load from field
                il.Emit(OpCodes.Call, propertyInfo.GetGetMethod());
                il.Emit(OpCodes.Ret);


                //set
                var valuePropertySet = typeBuilder.DefineMethod("set_" + propertyInfo.Name, getSetAttributes, typeof(void), new[] { propertyInfo.PropertyType });

                il = valuePropertySet.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);               
                il.Emit(OpCodes.Call, propertyInfo.GetSetMethod());

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldstr, propertyInfo.Name);
                il.Emit(OpCodes.Call, observePropertyType.GetMethod("OnPropertyChanged", new []{ typeof(string) }));
                
                il.Emit(OpCodes.Ret);

                property.SetGetMethod(valuePropertyGet);
                property.SetSetMethod(valuePropertySet);
            }

            //_assemblyBuilder.Save(_assemblyFileName);

            return typeBuilder.CreateType();
        }


        #region Private Methods

        private static void Initilaize(Type baseClass)
        {
            //const string extension = "dll";
            const string suffix = "_Seed";

            _moduleName = string.Format("{0}{1}", baseClass.Name, suffix);
            //_assemblyFileName = Path.Combine(_moduleName, extension);

            //var assemblyName = Debugger.IsAttached && File.Exists(_assemblyFileName)
            //    ? Assembly.LoadFile(_assemblyFileName).GetName()
            //    : new AssemblyName(_moduleName);
            var assemblyName = new AssemblyName(_moduleName);

            //_assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            _assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        }

        private static TypeBuilder GetSeedClassBuilder(Type baseClass)
        {
            Initilaize(baseClass);
            //var moduleBuilder = _assemblyBuilder.DefineDynamicModule(_moduleName, _assemblyFileName);
            var moduleBuilder = _assemblyBuilder.DefineDynamicModule(_moduleName);

            const TypeAttributes typeAttributes = TypeAttributes.Public |
                                                  TypeAttributes.Class |
                                                  TypeAttributes.AutoClass |
                                                  TypeAttributes.AnsiClass |
                                                  TypeAttributes.BeforeFieldInit |
                                                  TypeAttributes.AutoLayout;

            return moduleBuilder.DefineType(_moduleName, typeAttributes, baseClass);
        }

        #endregion
    }   
}
