using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starfish.Base.Interfaces
{
    public interface ITemplateMetadata
    {
        PatternBuilderTypeCode PatternTypeCode { get; set; }
        PatternBuilderLanguageCode LanguageCode { get; set; }
    }
}
