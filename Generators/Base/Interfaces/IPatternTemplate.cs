using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starfish.Structures;

namespace Starfish
{
    public interface IPatternTemplate
    {
        string NameSpace { get; set; }
        string ClassName { get; set; }
        List<MethodStruct> Methods { get; set; }
        string ReturnType { get; set; }
        string FileName { get; }
        PatternBuilderLanguageCode TargetLanguage { get; set; }
        string TransformText();
        void ClearCache();
    }
}
