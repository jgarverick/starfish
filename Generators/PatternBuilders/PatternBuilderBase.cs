using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starfish.Structures;
using Starfish.Templates.Projects;
using System.IO;
using System.Data;
using Starfish.Templates.Objects;

namespace Starfish.PatternBuilders
{
    public abstract class PatternBuilderBase:IPatternBuilder
    {
        #region Fields ---------------------------------------------------------

        protected PatternBuilderConfig Options;

        protected string FileExtension
        {
            get
            {
                switch (Options.TargetLanguage)
                {
                    case PatternBuilderLanguageCode.VB:
                        return ".vb";
                    case PatternBuilderLanguageCode.CSharp:
                        return ".cs";
                    case PatternBuilderLanguageCode.Java:
                    case PatternBuilderLanguageCode.JSharp:
                        return ".java";
                    case PatternBuilderLanguageCode.Python:
                        return ".py";
                    case PatternBuilderLanguageCode.Ruby:
                        return ".rb";
                    default:
                        return ".cs";
                }
            }
        }
        //
        #endregion

        #region Properties -----------------------------------------------------

        public virtual List<IPatternTemplate> Templates
        {
            get;
            set;
        }

        public virtual string ContractSource
        {
            get;
            set;
        }

        public virtual string ServiceSource
        {
            get;
            set;
        }
        //
        #endregion

        #region Methods --------------------------------------------------------

        public PatternBuilderBase()
        {
            Templates = new List<IPatternTemplate>();
        }

        public virtual void Initialize(Structures.PatternBuilderConfig config)
        {
            Options = config;
            InitializeDirectories();
        }

        public virtual void Execute()
        {
            if (System.IO.Directory.Exists(Options.SaveDirectory))
            {

                DataSet schema = DatabaseMetadataStore.GetMetadata(Options.DBServer, Options.DBInstance);
                // Get the tables collection
                DataTable tables = schema.Tables["TableColumnView"];
                string currentTableName = "";
                foreach (DataRow row in tables.Rows)
                {
                    if (row[0].ToString() != currentTableName)
                    {
                        currentTableName = row[0].ToString();
                        tables.DefaultView.RowFilter = string.Format("TableName = '{0}'", currentTableName);
                        string normalizedObjectName = currentTableName.Replace("_", "");
                        Console.WriteLine("Generating classes for {0}...", normalizedObjectName);
                        if (Options.MethodStructures.ContainsKey(currentTableName))
                            GetDefaultMethods(currentTableName).ForEach(method =>
                            {
                                if (!(Options.MethodStructures[currentTableName].Contains(method)))
                                    Options.MethodStructures[currentTableName].Add(method);
                            });

                        else
                            Options.MethodStructures.Add(row[0].ToString(), GetDefaultMethods(currentTableName));

                        Templates.ForEach(generator =>
                        {
                            generator.ClearCache();
                            if (generator is BusinessObject)
                                (generator as BusinessObject).ColumnsTable = tables.DefaultView.ToTable();
                            generator.ReturnType = row[0].ToString();
                            generator.ClassName = currentTableName;
                            generator.NameSpace = Options.ProjectNamespace;
                            generator.Methods = Options.MethodStructures.ContainsKey(currentTableName) ? Options.MethodStructures[currentTableName] : new List<MethodStruct>();
                            generator.TargetLanguage = Options.TargetLanguage;
                            WriteFile(Options.SaveDirectory + generator.FileName + FileExtension, generator.TransformText());

                        });
                    }
                }

            }
        }

        protected virtual void WriteFile(string path, string contents)
        {
            using (System.IO.StreamWriter tw = new System.IO.StreamWriter(path))
            {
                tw.Write(contents);
                tw.Close();
            }

        }

        public abstract void InitializeDirectories();

        protected void CreateProject(string projTypeName, List<string> directories)
        {
            string projBase = Options.SaveDirectory + "\\" + Options.ProjectNamespace + "." + projTypeName + "\\";

            ProjectTemplate proj = new ProjectTemplate();
            proj.ProjectGuid = Guid.NewGuid();
            proj.Namespace = Options.ProjectNamespace;
            proj.IncludedFiles = new List<string>();
            directories.ForEach(dir =>
            {
                Directory.GetFiles(projBase + "\\" + dir).ToList().ForEach(file =>
                {
                    if (string.IsNullOrEmpty(dir))
                        proj.IncludedFiles.Add(Path.GetFileName(file));
                    else
                        proj.IncludedFiles.Add(dir + "\\" + Path.GetFileName(file));
                });
            });
            WriteFile(projBase + Options.ProjectNamespace + "." + projTypeName + FileExtension + "proj", proj.TransformText());
        }

        protected List<MethodStruct> GetDefaultMethods(string objName)
        {
            List<MethodStruct> methods = new List<MethodStruct>();
            MethodStruct add = new MethodStruct();
            add.Name = "Add" + objName.Replace("_", "");
            add.ClassName = objName;
            MethodStruct update = new MethodStruct();
            update.Name = "Update" + objName.Replace("_", "");
            update.ClassName = objName;
            MethodStruct get = new MethodStruct();
            get.Name = "Get" + objName.Replace("_", "");
            get.ClassName = objName;
            methods.Add(add);
            methods.Add(update);
            methods.Add(get);
            return methods;
        }
        //
        #endregion
    }
}
