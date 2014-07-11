using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starfish.Structures;
using Starfish.Base;

namespace Starfish.Templates.Objects
{
    [UseWithLanguage(PatternBuilderLanguageCode.CSharp)]
    [UseWithLanguage(PatternBuilderLanguageCode.VB)]
    [UseWithPattern(PatternBuilderTypeCode.Custom)]
    public partial class Contract:IPatternTemplate
    {
        public void ClearCache()
        {
            base.GenerationEnvironment.Clear();
        }

        public string NameSpace
        {
            get { return this._namespaceField; }
            set { this._namespaceField = value; }
        }
        public string ClassName
        {
            get { return this._classNameField; }
            set { this._classNameField = value; }
        }

        public List<MethodStruct> Methods
        {
            get { return this._methodsField; }
            set { this._methodsField = value; }
        }


        public string ReturnType
        {
            get;
            set;
        }

        public string FileName { get { return NameSpace+ ".Services\\Contracts\\" + "I" + ClassName + "Service"; } }


        public PatternBuilderLanguageCode TargetLanguage
        {
            get { return this._languageField; }
            set { this._languageField = value; }
        }
    }
}
