using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace UseAbilities.MVVM.Base
{
    /// <summary>
    /// Not tested with complicated get methods
    /// </summary>
    public class ObserveWrapper 
    {
        private AssemblyBuilder _assemblyBuilder;
        private string _name;

        private TypeBuilder GetSeedClassBuilder(Type baseClass)
        {
            _name = string.Format("{0}_Seed", baseClass.Name);
            var asmName = new AssemblyName(_name);
            _assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
            var mb = _assemblyBuilder.DefineDynamicModule(_name, _name + ".dll");

            const TypeAttributes typeAttributes = TypeAttributes.Public |
                                                  TypeAttributes.Class |
                                                  TypeAttributes.AutoClass |
                                                  TypeAttributes.AnsiClass |
                                                  TypeAttributes.BeforeFieldInit |
                                                  TypeAttributes.AutoLayout;

            return mb.DefineType(_name, typeAttributes, baseClass);
        }

        public Type Resolve(Type observePropertyType)
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


            var resultType = typeBuilder.CreateType();
            _assemblyBuilder.Save(_name + ".dll");

            return resultType;
        }
    }
}
