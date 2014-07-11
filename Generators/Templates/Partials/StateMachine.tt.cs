using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starfish.Base;

namespace Starfish.Templates.Objects
{
    [UseWithPattern(PatternBuilderTypeCode.StateMachine)]
    public partial class StateMachine:IPatternTemplate
    {
        public string NameSpace
        {
            get;
            set;
        }

        public string ClassName
        {
            get;
            set;
        }

        public List<Structures.MethodStruct> Methods
        {
            get;
            set;
        }

        public string ReturnType
        {
            get;
            set;
        }

        public string FileName
        {
            get { throw new NotImplementedException(); }
        }

        public PatternBuilderLanguageCode TargetLanguage
        {
            get;
            set;
        }

        public void ClearCache()
        {
            base.GenerationEnvironment.Clear(); 
        }
    }
}
