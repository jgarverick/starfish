using System.Collections.Generic;
using System.Linq;
using Starfish.Structures;
using Starfish.Templates.Objects;
using System.Reflection;
using Starfish.Base;
using Starfish.PatternBuilders;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System;

namespace Starfish
{
    public class PatternResolver
    {
        private static IWindsorContainer Resolver;
        private static PatternBuilderLanguageCode targetLanguage;
        private static PatternBuilderTypeCode patternType;

        static PatternResolver()
        {
            Resolver = new WindsorContainer();
            Resolver.RegisterType(typeof(IPatternTemplate), typeof(Contract), "Contract");
            Resolver.RegisterType(typeof(IPatternTemplate), typeof(BusinessObject), "BusinessObject");
            Resolver.RegisterType(typeof(IPatternBuilder), typeof(CustomPatternBuilder), "CustomPatternBuilder");
            Resolver.RegisterType(typeof(IPatternBuilder), typeof(PocoPatternBuilder), "PocoPatternBuilder");
            Resolver.RegisterType(typeof(IPatternBuilder), typeof(DataModelBuilder), "DataModelBuilder");
            Resolver.RegisterType(typeof(IPatternBuilder), typeof(HibernatePatternBuilder), "HibernatePatternBuilder");
            Resolver.RegisterType(typeof(IPatternBuilder), typeof(ServiceAdapterBuilder), "ServiceAdapterBuilder");
            Resolver.RegisterType(typeof(IPatternBuilder), typeof(ServiceFactoryBuilder), "ServiceFactoryBuilder");
            Resolver.RegisterType(typeof(IPatternBuilder), typeof(StateMachinePatternBuilder), "StateMachinePatternBuilder");

        }



        public static IPatternBuilder Resolve(PatternBuilderConfig options)
        {
            patternType = options.PatternType;
            targetLanguage = options.TargetLanguage;
            IPatternBuilder retval = ResolveBuilder();
            retval.Initialize(options);
            return retval;
        }

        public static List<IPatternTemplate> Resolve(string className, PatternBuilderConfig options)
        {
            patternType = options.PatternType;
            targetLanguage = options.TargetLanguage;

            List<IPatternTemplate> templates = new List<IPatternTemplate>();

            templates.AddRange(ResolvePatternTemplates());
            
            // Now get the template generators
            templates.ForEach(template =>
            {
                template.ClassName = className;
                template.NameSpace = options.ProjectNamespace;
                template.Methods = options.MethodStructures.ContainsKey(className) ? options.MethodStructures[className] : new List<MethodStruct>();
                template.ReturnType = className;
                template.TargetLanguage = options.TargetLanguage;
                
            });
            return templates;
        }

        private static IPatternBuilder ResolveBuilder()
        {
            string builderName = string.Empty;
            switch (patternType)
            {
                case PatternBuilderTypeCode.Custom:
                    builderName = "CustomPatternBuilder";
                    break;
                case PatternBuilderTypeCode.FullDataModel:
                    builderName = "DataModelBuilder";
                    break;
                case PatternBuilderTypeCode.Hibernate:
                    builderName = "HibernatePatternBuilder";
                    break;
                case PatternBuilderTypeCode.POCO:
                    builderName = "PocoPatternBuilder";
                    break;
                case PatternBuilderTypeCode.ServiceAdapter:
                    builderName = "ServiceAdapterBuilder";
                    break;
                case PatternBuilderTypeCode.ServiceFactory:
                    builderName = "ServiceFactoryBuilder";
                    break;
                case PatternBuilderTypeCode.StateMachine:
                    builderName = "StateMachinePatternBuilder";
                    break;
                default:
                    builderName = "CustomPatternBuilder";
                    break;
            }
            IPatternBuilder builder = Resolver.Resolve<IPatternBuilder>(builderName);
            builder.Templates = ResolvePatternTemplates();
            return builder;
        }


        private static List<IPatternTemplate> ResolvePatternTemplates()
        {
            List<IPatternTemplate> templates = new List<IPatternTemplate>();
            Assembly assem = Assembly.GetAssembly(typeof(PatternResolver));
            assem.Implements<IPatternTemplate>().ToList().ForEach(template =>
            {
                var query = assem.AttributedWith<UseWithLanguageAttribute>()
                    .Where(x => x.GetTargetLanguages().Where(y => y.LanguageCode == targetLanguage).Any()
                    && x.GetPatternBuilderTypes().Where(z=>z.PatternTypeCode == patternType).Any());
                if (query.Any())
                {
                    query.ToList().ForEach(q =>
                    {
                        IPatternTemplate temp = Resolver.Resolve<IPatternTemplate>(q.Name);
                        if (!(templates.Where(x=>x.GetType().Name == q.Name).Any()))
                            templates.Add(temp);
                    });
                }
            });
            return templates;
        }
    }
}
