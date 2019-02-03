using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace AuthService.Bootstrappers
{
    public class PluginsBootstrapper : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = GetPluginAssemblies();
            builder.RegisterAssemblyModules(assemblies);
        }

        private Assembly[] GetPluginAssemblies()
        {
            return Directory.EnumerateFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "*Module.dll", SearchOption.AllDirectories)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .ToArray();
        }

    }
}
