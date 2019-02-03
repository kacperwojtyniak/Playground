using Autofac;
using System;

namespace Demos.Common
{
    public static class IoC
    {
        public static IContainer Container {get; private set;}
        public static void InitializeIoC(IContainer container)
        {
            Container = container;            
        }

        public static T Resolve<T>()
        {
           return Container.Resolve<T>();
        }
    }
}
