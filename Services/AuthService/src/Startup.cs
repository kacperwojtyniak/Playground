using AuthService.Configurations;
using AuthService.FakeDataGeneration;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Database.Common.Configuration;
using Demos.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimpleJwtProvider.Extensions;
using System;
using System.Reflection;

namespace AuthService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.ConfigureSimpleJwtProvider(Configuration);

            services.Configure<DbConfig>(Configuration.GetSection("DbConfig"));
            services.AddScoped(cfg => cfg.GetService<IOptions<DbConfig>>().Value);

            services.Configure<RedisConfiguration>(Configuration.GetSection("RedisConfiguration"));
            services.AddScoped(cfg => cfg.GetService<IOptions<RedisConfiguration>>().Value);
            
            services.Configure<TokensConfiguration>(Configuration.GetSection("TokensConfiguration"));
            services.AddScoped(cfg => cfg.GetService<IOptions<TokensConfiguration>>().Value);

            
            var builder = new ContainerBuilder();

            builder
                .RegisterAssemblyModules(Assembly.GetEntryAssembly());

            builder
                .Populate(services);

            var container = builder.Build();
            IoC.InitializeIoC(container);

            //var fakeData = IoC.Resolve<IFakeDataGenerator>();
            //fakeData.GenerateFakeUsers();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }


    }
}
