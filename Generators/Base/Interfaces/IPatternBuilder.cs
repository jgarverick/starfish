using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starfish.Structures;

namespace Starfish
{
    public interface IPatternBuilder
    {
        void Execute();
        string ContractSource { get; set; }
        string ServiceSource { get; set; }
        void Initialize(PatternBuilderConfig config);
        List<IPatternTemplate> Templates { get; set; }
    }
}
