using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starfish
{
    public enum PatternBuilderTypeCode
    {
        Custom,
        FullDataModel,
        ServiceFactory,
        ServiceAdapter,
        Composite,
        POCO,
        Hibernate,
        StateMachine,
    }

    public enum PatternBuilderLanguageCode
    {
        CSharp,
        VB,
        CPlusPlus,
        JSharp,
        Java,
        Ruby,
        Python,
    }

    [Flags]
    public enum PatternBuilderOptions
    {
        CreateVSProjFile = 0x1,
        CompileCodeAfterGeneration = 0x2,
        GenerateManagerPerTable = 0x4,
    }
}
