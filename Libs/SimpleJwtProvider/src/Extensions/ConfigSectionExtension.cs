using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimpleJwtProvider.Configuration;

namespace SimpleJwtProvider.Extensions
{
    public static class ConfigSectionExtension
    {

        public static void ConfigureSimpleJwtProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SimpleJwtProviderConfig>(configuration.GetSection("SimpleJwtProviderConfig"));

            services.AddScoped(cfg => cfg.GetService<IOptions<SimpleJwtProviderConfig>>().Value);
        }
    }
}
