﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Starfish.Base;

namespace Starfish.Templates.Objects
{
    [UseWithLanguage(PatternBuilderLanguageCode.CSharp)]
    [UseWithLanguage(PatternBuilderLanguageCode.VB)]
    [UseWithPattern(PatternBuilderTypeCode.Custom)]
    [UseWithPattern(PatternBuilderTypeCode.FullDataModel)]
    [UseWithPattern(PatternBuilderTypeCode.POCO)]
    [UseWithPattern(PatternBuilderTypeCode.Hibernate)]
    public partial class BusinessObject:IPatternTemplate
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
            get { return NameSpace + ".Business\\Business\\" + ClassName; }
        }

        public DataTable ColumnsTable
        {
            get { return this._columnsField; }
            set { this._columnsField = value; }
        }


        public PatternBuilderLanguageCode TargetLanguage
        {
            get { return this._languageField; }
            set { this._languageField = value; }
        }
    }
}
