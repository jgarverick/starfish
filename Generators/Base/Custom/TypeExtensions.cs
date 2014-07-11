using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Starfish.Base;
using Extensionista;

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
    }
}
