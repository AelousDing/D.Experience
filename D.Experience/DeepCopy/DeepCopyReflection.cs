using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace D.Experience.DeepCopy
{
    /// <summary>
    /// 代码来自http://www.tuicool.com/articles/beu2InZ,做了一些修改，将反射的时候取字段，而不取属性，因为属性最终暴漏出来的是字段。
    /// 修改后支持泛型的深拷贝。源代码泛型的深拷贝报错。
    /// </summary>
    public static class DeepCopyReflection
    {
        /// <summary>
        /// 利用反射实现深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>

        public static T DeepCopyWithReflection<T>(this T obj)
        {
            Type type = obj.GetType();
            // 如果是字符串或值类型则直接返回
            if (obj is string || type.IsValueType) return obj;
            if (type.IsArray)
            {
                Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    if (array.GetValue(i) == null)
                    {
                        continue;
                    }
                    copied.SetValue(DeepCopyWithReflection(array.GetValue(i)), i);
                }
                return (T)Convert.ChangeType(copied, type);
            }
            object retval = Activator.CreateInstance(type);
            FieldInfo[] fields = type.GetFields(
              BindingFlags.Public | BindingFlags.NonPublic
              | BindingFlags.Instance | BindingFlags.Static);
            foreach (var field in fields)
            {
                if (field.IsLiteral || field.IsInitOnly)//常量或只读变量排除
                {
                    continue;
                }
                var propertyValue = field.GetValue(obj);
                if (propertyValue == null)
                    continue;
                field.SetValue(retval, DeepCopyWithReflection(propertyValue));
            }
            return (T)retval;
        }
    }
}
