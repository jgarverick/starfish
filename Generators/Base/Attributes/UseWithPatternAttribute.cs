using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starfish.Base
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class UseWithPatternAttribute:Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly PatternBuilderTypeCode typeCode;

        // This is a positional argument
        public UseWithPatternAttribute(PatternBuilderTypeCode patternType)
        {
            this.typeCode = patternType;

            // TODO: Implement code here
        }

        public PatternBuilderTypeCode PatternTypeCode
        {
            get { return typeCode; }
        }

    }
}
