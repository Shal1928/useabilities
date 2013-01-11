using System;
using System.Collections;

namespace UseAbilities.DotNet.Reflection
{
    public abstract class CloneableObject : ICloneable
    {
        public object Clone()
        {
            var newObject = Activator.CreateInstance(GetType());

            var properties = newObject.GetType().GetProperties();

            var i = 0;

            foreach (var propertyInfo in GetType().GetProperties())
            {
                var cloneType = propertyInfo.PropertyType.GetInterface("ICloneable", true);

                if (cloneType != null)
                {
                    var clone = (ICloneable)propertyInfo.GetValue(this, null);
                    properties[i].SetValue(newObject, clone.Clone(), null);
                }
                else properties[i].SetValue(newObject, propertyInfo.GetValue(this, null), null);
                
                var enumerableType = propertyInfo.PropertyType.GetInterface("IEnumerable", true);
                if (enumerableType != null)
                {
                    var IEnum = (IEnumerable)propertyInfo.GetValue(this, null);

                    var listType = properties[i].PropertyType.GetInterface("IList", true);
                    var dicType = properties[i].PropertyType.GetInterface("IDictionary", true);
                    var j = 0;

                    if (listType != null)
                    {
                        var list = (IList)properties[i].GetValue(newObject, null);

                        foreach (var obj in IEnum)
                        {
                            cloneType = obj.GetType().GetInterface("ICloneable", true);

                            if (cloneType != null) list[j] = ((ICloneable)obj).Clone();
                            j++;
                        }
                    }
                    else if (dicType != null)
                    {
                        var dic = (IDictionary)properties[i].GetValue(newObject, null);
                        j = 0;

                        foreach (DictionaryEntry de in IEnum)
                        {
                            cloneType = de.Value.GetType().GetInterface("ICloneable", true);

                            if (cloneType != null) dic[de.Key] = ((ICloneable)de.Value).Clone();
                            j++;
                        }
                    }
                }
                i++;
            }
            return newObject;
        }

        //public object Clone()
        //{
        //    var newObject = Activator.CreateInstance(GetType());

        //    var fields = newObject.GetType().GetFields();

        //    var i = 0;

        //    foreach (var fieldInfo in GetType().GetFields())
        //    {
        //        var cloneType = fieldInfo.FieldType.GetInterface("ICloneable", true);

        //        if (cloneType != null)
        //        {
        //            var clone = (ICloneable)fieldInfo.GetValue(this);
        //            fields[i].SetValue(newObject, clone.Clone());
        //        }
        //        else fields[i].SetValue(newObject, fieldInfo.GetValue(this));

        //        var enumerableType = fieldInfo.FieldType.GetInterface("IEnumerable", true);
        //        if (enumerableType != null)
        //        {
        //            var IEnum = (IEnumerable)fieldInfo.GetValue(this);

        //            var listType = fields[i].FieldType.GetInterface("IList", true);
        //            var dicType = fields[i].FieldType.GetInterface("IDictionary", true);
        //            var j = 0;

        //            if (listType != null)
        //            {
        //                var list = (IList)fields[i].GetValue(newObject);

        //                foreach (var obj in IEnum)
        //                {
        //                    cloneType = obj.GetType().GetInterface("ICloneable", true);

        //                    if (cloneType != null)
        //                    {
        //                        var clone = (ICloneable)obj;
        //                        list[j] = clone.Clone();
        //                    }

        //                    j++;
        //                }
        //            }
        //            else if (dicType != null)
        //            {
        //                var dic = (IDictionary)fields[i].GetValue(newObject);
        //                j = 0;

        //                foreach (DictionaryEntry de in IEnum)
        //                {

        //                    cloneType = de.Value.GetType().
        //                        GetInterface("ICloneable", true);

        //                    if (cloneType != null)
        //                    {
        //                        var clone = (ICloneable)de.Value;

        //                        dic[de.Key] = clone.Clone();
        //                    }
        //                    j++;
        //                }
        //            }
        //        }
        //        i++;
        //    }
        //    return newObject;
        //}
    }
}
