using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using Starfish;
using Starfish.Structures;
using System.Data;
using Starfish.Templates;
using System.IO;
using Starfish.Templates.Projects;
using Starfish.Templates.Objects;
using System.ComponentModel.Composition;

namespace Starfish.PatternBuilders
{
    public class CustomPatternBuilder:PatternBuilderBase
    {

        public CustomPatternBuilder():base() {  }

        public override void Execute()
        {
            base.Execute();
            if (System.IO.Directory.Exists(Options.SaveDirectory))
            {
                if (Options.BuilderOptions.HasFlag(PatternBuilderOptions.CreateVSProjFile))
                {
                    CreateBusinessProject();
                    CreateCoreProject();
                    CreateServicesProject();
                }
            }
        }

        public override void InitializeDirectories()
        {
            string servicesSln = Options.SaveDirectory + "\\" + Options.ProjectNamespace + ".Services";
            string bizSln = Options.SaveDirectory + "\\" + Options.ProjectNamespace + ".Business";
            string coreSln = Options.SaveDirectory + "\\" + Options.ProjectNamespace + ".Core";
            // Delete everything first
            if (Directory.Exists(Options.SaveDirectory))
                System.IO.Directory.Delete(Options.SaveDirectory, true);
            Directory.CreateDirectory(Options.SaveDirectory);
            Directory.CreateDirectory(servicesSln);
            Directory.CreateDirectory(bizSln);
            Directory.CreateDirectory(coreSln);
            Directory.CreateDirectory(servicesSln + "\\Services");
            Directory.CreateDirectory(servicesSln+ "\\Contracts");
            Directory.CreateDirectory(coreSln + "\\Managers");
            Directory.CreateDirectory(bizSln + "\\Business");
            Directory.CreateDirectory(coreSln + "\\Helpers");
            Directory.CreateDirectory(servicesSln + "\\Returns");
        }

        public void CreateBusinessProject()
        {
            CreateProject("Business", new List<string>() { "Business" });
        }

        public void CreateCoreProject()
        {
            CreateProject("Core", new List<string>() {"Managers","Helpers", "..\\" });
        }

        public void CreateServicesProject()
        {
            CreateProject("Services", new List<string>() { "Services", "Contracts", "Returns" });
        }


    }
}
