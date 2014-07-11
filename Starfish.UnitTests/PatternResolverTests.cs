using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Starfish.Structures;

namespace Starfish.UnitTests
{
    [TestFixture]
    public class PatternResolverTests
    {
        [Test]
        public void TestResolve()
        {
            string[] patterns = Enum.GetNames(typeof(PatternBuilderTypeCode));
            string[] languages = Enum.GetNames(typeof(PatternBuilderLanguageCode));
            foreach (var p in patterns)
            {
                foreach (var l in languages)
                {
                    TestResolveBase((PatternBuilderLanguageCode)Enum.Parse(typeof(PatternBuilderLanguageCode), l),
                        (PatternBuilderTypeCode)Enum.Parse(typeof(PatternBuilderTypeCode), p));
                }
            }
        }

        private void TestResolveBase(PatternBuilderLanguageCode language,
            PatternBuilderTypeCode pattern)
        {
            PatternBuilderConfig config = new PatternBuilderConfig();
            config.BuilderOptions = PatternBuilderOptions.CreateVSProjFile;
            config.TargetLanguage = language;
            config.SaveDirectory = "C:\\output\\testing\\";
            config.PatternType = pattern;
            IPatternBuilder builder = PatternResolver.Resolve(config);
            Assert.IsNotNull(builder);
            Assert.IsNotNull(builder.Templates);
        }

    }
}
