using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starfish.Base
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class UseWithLanguageAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly PatternBuilderLanguageCode languageCode;

        // This is a positional argument
        public UseWithLanguageAttribute(PatternBuilderLanguageCode languageCode)
        {
            this.languageCode = languageCode;

            // TODO: Implement code here
        }

        public PatternBuilderLanguageCode LanguageCode
        {
            get { return languageCode; }
        }

    }
}
