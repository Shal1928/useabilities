using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;

namespace UseAbilities.MVVM.Base
{

    public class EntityA
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class EntityB
    {
        public long MyId { get; set; }
        public string MyName { get; set; }
    }

//    class MyClass {
//    public Action<int> MyAction = x => { throw new NotImplementedException() };
//}
//To allow the action to be overridden:

//MyClass myObj = new MyClass();
//myObj.MyAction = (x) => { Console.WriteLine(x); };

    public class ObserveWrapper 
    {
        //NotifyPropertyChanged
        //public T Resolve()
        //{
            
        //}

        //public static void Resolve<T>(this T objectValue) where T : ObserveProperty
        //{
            
        //}

        private AssemblyBuilder _assemblyBuilder;
        private string _name;

        private TypeBuilder GetSeedClassBuilder(Type baseClass)
        {
            var myDomain = Thread.GetDomain();
            var assemblyName = Assembly.GetAssembly(baseClass);
            //var myAsmBuilder = myDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            //baseClass.
            // Generate a persistable single-module assembly.
            //var myModBuilder = myAsmBuilder.DefineDynamicModule(assemblyName.Name, assemblyName.Name + ".dll");


            //AssemblyQualifiedName = "Factime.ViewModels.MainWindowViewModel, Factime, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null"
            //Assembly = {Factime, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null}

            //Assembly = {Factime, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null}
            //FullyQualifiedName = "C:\\Archive\\HW\\Factime\\Factime\\bin\\Release\\Factime.exe"

            _name = string.Format("{0}_Seed", baseClass.Name);
            var asmName = new AssemblyName(_name);
            _assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
            //_assemblyBuilder = myDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
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

            //ConstructorBuilder constructor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[0]);
            
            //var il = constructor.GetILGenerator();
            //ConstructorInfo baseConstructor = observePropertyType.GetConstructor(new Type[0]);
            //il.Emit(OpCodes.Ldarg_0);
            //il.Emit(OpCodes.Call, baseConstructor);
            //il.Emit(OpCodes.Ret);

            //newProp.SetSetMethod(pSet);
            //newProp.SetGetMethod(pGet);

            foreach (var propertyInfo in propertyInfoCollection.Where(pi => pi.GetSetMethod() != null && pi.GetSetMethod().IsVirtual))
            {
                var valueField = typeBuilder.DefineField("_" + propertyInfo.Name, propertyInfo.PropertyType, FieldAttributes.Private);

                var property = typeBuilder.DefineProperty(propertyInfo.Name, PropertyAttributes.None, propertyInfo.PropertyType, null);

                //get
                var valuePropertyGet = typeBuilder.DefineMethod("get_" + propertyInfo.Name, getSetAttributes, null, new[] { propertyInfo.PropertyType });

                var il = valuePropertyGet.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, valueField); //load from field
                il.Emit(OpCodes.Ret);

                
                //typeBuilder.DefineMethodOverride(valuePropertyGet, observePropertyType.GetMethod("get_" + propertyInfo.Name));


                //set
                var valuePropertySet = typeBuilder.DefineMethod("set_" + propertyInfo.Name, getSetAttributes, null, new[] { propertyInfo.PropertyType });


                //il.Emit(OpCodes.Ldarg_0);
                //il.Emit(OpCodes.Ldarg_1);
                //il.Emit(OpCodes.Ldstr, "Overridning StringProperty");
                //il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
                //il.Emit(OpCodes.Call, typeof(A).GetProperty("StringProperty").GetSetMethod());

                il = valuePropertySet.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);
                
                
                Type[] propertyChangedParameters = { typeof(string) };
                var propertyChanged = observePropertyType.GetMethod("OnPropertyChanged", propertyChangedParameters);
                //var a = propertyChanged;
                //a.Invoke();
                
                il.Emit(OpCodes.Call, propertyInfo.GetSetMethod());

                il.Emit(OpCodes.Ldarg_2);
                il.Emit(OpCodes.Ldstr, propertyInfo.Name);
                il.Emit(OpCodes.Call, observePropertyType.GetMethod("OnPropertyChanged", propertyChangedParameters));
                
                il.Emit(OpCodes.Ret);

                //typeBuilder.DefineMethodOverride(valuePropertySet, observePropertyType.GetMethod("set_" + propertyInfo.Name));

                property.SetGetMethod(valuePropertyGet);
                property.SetSetMethod(valuePropertySet);
            }


            var resultType = typeBuilder.CreateType();
            _assemblyBuilder.Save(_name + ".dll");

            //var i = Activator.CreateInstance(resultType);
            //var i = resultType.BaseType;
            //return i;
            return resultType;
        }

    //public static class MyTypeBuilder
    //{
    //    public static void CreateNewObject()
    //    {
    //        var myType = CompileResultType();
    //        var myObject = Activator.CreateInstance(myType);
    //    }

    //    public static Type CompileResultType(Type baseType)
    //    {
    //        TypeBuilder tb = GetTypeBuilder();
    //        ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

    //        // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
    //        foreach (var field in yourListOfFields)
    //            CreateProperty(tb, field.FieldName, field.FieldType);

    //        Type objectType = tb.CreateType();
    //        return objectType;
    //    }

    //    private static TypeBuilder GetTypeBuilder()
    //    {
    //        var typeSignature = "MyDynamicType";
    //        var an = new AssemblyName(typeSignature);
    //        AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
    //        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
    //        TypeBuilder tb = moduleBuilder.DefineType(typeSignature
    //                            , TypeAttributes.Public |
    //                            TypeAttributes.Class |
    //                            TypeAttributes.AutoClass |
    //                            TypeAttributes.AnsiClass |
    //                            TypeAttributes.BeforeFieldInit |
    //                            TypeAttributes.AutoLayout
    //                            , null);
    //        return tb;
    //    }

    //    private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
    //    {
    //        FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

    //        PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
    //        MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
    //        ILGenerator getIl = getPropMthdBldr.GetILGenerator();

    //        getIl.Emit(OpCodes.Ldarg_0);
    //        getIl.Emit(OpCodes.Ldfld, fieldBuilder);
    //        getIl.Emit(OpCodes.Ret);

    //        MethodBuilder setPropMthdBldr =
    //            tb.DefineMethod("set_" + propertyName,
    //              MethodAttributes.Public |
    //              MethodAttributes.SpecialName |
    //              MethodAttributes.HideBySig,
    //              null, new[] { propertyType });

    //        ILGenerator setIl = setPropMthdBldr.GetILGenerator();
    //        Label modifyProperty = setIl.DefineLabel();
    //        Label exitSet = setIl.DefineLabel();

    //        setIl.MarkLabel(modifyProperty);
    //        setIl.Emit(OpCodes.Ldarg_0);
    //        setIl.Emit(OpCodes.Ldarg_1);
    //        setIl.Emit(OpCodes.Stfld, fieldBuilder);

    //        setIl.Emit(OpCodes.Nop);
    //        setIl.MarkLabel(exitSet);
    //        setIl.Emit(OpCodes.Ret);

    //        propertyBuilder.SetGetMethod(getPropMthdBldr);
    //        propertyBuilder.SetSetMethod(setPropMthdBldr);
    //    }
    //}


        //private static bool SetField<T>(ref T field, T value, string propertyName)
        //{
        //    if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        //    field = value;
        //    OnPropertyChanged(propertyName);
        //    return true;
        //}

        //public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
        //{
        //    StringBuilder paramInfo = new StringBuilder();
        //    paramInfo.AppendFormat("Calling {0}(", binder.Name);
        //    foreach (var item in args)
        //        paramInfo.AppendFormat("{0}, ", item.Value);
        //    paramInfo.Append(")");

        //    Expression[] parameters = new Expression[]
        //    {
        //        Expression.Constant(paramInfo.ToString())
        //    };

        //    DynamicMetaObject methodInfo = new DynamicMetaObject(
        //        Expression.Call(
        //        Expression.Convert(Expression, LimitType),
        //        typeof(DynamicDictionary).GetMethod("WriteMethodInfo"),
        //        parameters),
        //        BindingRestrictions.GetTypeRestriction(Expression, LimitType));
        //    return methodInfo;
        //}

        //private static object GetInstance(ObserveProperty registeredObject)
        //{
        //    var instance = registeredObject.Instance;
        //    if (instance == null)
        //    {
        //        var injectedProperties = registeredObject.ConcreteType.GetProperties(BINDING_FLAGS_ALL_INSTANCE_CTORS);
        //        var properties = injectedProperties.Where(injectedProperty =>
        //                                                  injectedProperty.GetCustomAttributes(
        //                                                      typeof(InjectedProperty), false).Any()).ToList();

        //        if (properties.Any())
        //        {
        //            var propertiesMappingDic = properties.ToDictionary(property => property,
        //                                                               property => ResolveObject(property.PropertyType));

        //            instance = registeredObject.CreateInstance(propertiesMappingDic);
        //        }
        //        else
        //        {
        //            var constructorInfo =
        //                registeredObject.ConcreteType.GetConstructors(BINDING_FLAGS_ALL_INSTANCE_CTORS).First();
        //            var parameters =
        //                constructorInfo.GetParameters().Select(parameter => ResolveObject(parameter.ParameterType));
        //            instance = registeredObject.CreateInstance(constructorInfo, parameters.ToArray());
        //        }
        //    }

        //    return instance;
        //}
    }
}
