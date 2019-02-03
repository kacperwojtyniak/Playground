using Autofac;
using Database.Common.ContextProvider;
using Database.Common.Contexts;
using MongoDbAdapterModule.Contexts;
using MongoDbAdapterModule.Provider;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbAdapterModule.Bootstrapper
{
    class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MongoContextProvider>()
                .As<IContextProvider>()
                .InstancePerDependency();

            builder
               .RegisterGeneric(typeof(MongoContext<>))
               .As(typeof(IMongoContext<>))
               .InstancePerDependency();
        }
    }
}
