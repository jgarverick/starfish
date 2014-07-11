using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Starfish;
using Starfish.Base;
using Starfish.Structures;

namespace Starfish.TestApp
{
     
    class Program
    {
        static void Main(string[] args)
        {
            PatternBuilderConfig config = new PatternBuilderConfig();
            config.PatternType = PatternBuilderTypeCode.Hibernate;
            config.TargetLanguage = PatternBuilderLanguageCode.CSharp;
            config.SaveDirectory = "C:\\test\\";
            config.ProjectName = "Email";
            config.ProjectNamespace = "Email";
            config.DBServer = ".\\SQLEXPRESS";
            config.DBInstance = "tetsuo";
            config.MethodStructures = new Dictionary<string, List<MethodStruct>>();
            config.BuilderOptions = PatternBuilderOptions.CreateVSProjFile;
            IPatternBuilder builder = PatternResolver.Resolve(config);
            builder.Execute();
            
           
        }
    }
}
