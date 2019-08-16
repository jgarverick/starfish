using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Starfish.Base;

namespace Starfish
{
    public static class TypeExtensions
    {
        public static UseWithPatternAttribute[] GetPatternBuilderTypes(this Type type)
        {
            List<UseWithPatternAttribute> code = new List<UseWithPatternAttribute>();
            object[] attr = type.GetCustomAttributes(typeof(UseWithPatternAttribute), true);
            if (attr.Any())
            {
                attr.ToList().ForEach(a => code.Add(a as UseWithPatternAttribute));
            }
            return code.ToArray();
        }

        public static UseWithLanguageAttribute[] GetTargetLanguages(this Type type)
        {
            List<UseWithLanguageAttribute> code = new List<UseWithLanguageAttribute>();
            object[] attr = type.GetCustomAttributes(typeof(UseWithLanguageAttribute), true);
            if (attr.Any())
            {
                attr.ToList().ForEach(a => code.Add(a as UseWithLanguageAttribute));
            }
            return code.ToArray();
        }

        public static bool AttributedWith<T>(this Type type)
        {
            if (typeof(Attribute).IsAssignableFrom(typeof(T)))
                return type.GetCustomAttributes(typeof(T), true).Any();
            else
                return false;
        }

        public static bool Implements<T>(this Type type)
        {
            if (typeof(T).IsInterface)
                return type.GetInterface(typeof(T).Name) != null;
            else
                return false;
        }


        public static Type[] Implements<T>(this Assembly asm)
        {
            List<Type> types = new List<Type>();
            asm.GetTypes().ToList().ForEach(type =>
            {
                if (type.Implements<T>())
                    types.Add(type);
            });
            return types.ToArray();
        }

        public static Type[] AttributedWith<T>(this Assembly asm)
        {
            List<Type> types = new List<Type>();
            asm.GetTypes().ToList().ForEach(type =>
            {
                if (type.AttributedWith<T>())
                    types.Add(type);
            });
            return types.ToArray();
        }
    }
}
