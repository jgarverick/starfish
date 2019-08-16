using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starfish
{
    public static class ContainerExtensions
    {
        public static void RegisterType(this IWindsorContainer container, Type interfaceType, Type implementationType, string name)
        {
            var s = interfaceType.GetType();
            var component = Component.For(interfaceType).ImplementedBy(implementationType).Named(name).LifestyleTransient();
            container.Register(component);
        }
    }
}
