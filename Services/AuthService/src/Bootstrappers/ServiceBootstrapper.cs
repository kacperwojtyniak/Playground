using AuthService.FakeDataGeneration;
using AuthService.Interfaces;
using AuthService.Repositories;
using AuthService.Services;
using Autofac;
using SimpleJwtProvider.Interfaces;
using SimpleJwtProvider.Services.HeaderService;
using SimpleJwtProvider.Services.HeaderService.HeaderCreators;
using SimpleJwtProvider.Services.PayloadService;
using SimpleJwtProvider.Services.TokenService;

namespace AuthService.Bootstrappers
{
    public class ServiceBootstrapper : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HeaderProvider<HMACHeaderCreator>>()
                .As<IHeaderProvider>();

            builder
                .RegisterType<PayloadProvider>()
                .As<IPayloadProvider>();

            builder
                .RegisterType<AccessTokenProvider>()
                .As<IAccessTokenProvider>();

            builder
                .RegisterType<RefreshTokenProvider>()
                .As<IRefreshTokenProvider>();

            builder
                .RegisterType<PasswordHasher>()
                .As<IPasswordHasher>();

            builder
                .RegisterType<PasswordValidator>()
                .As<IPasswordValidator>();

            builder
                .RegisterType<AuthenticationService>()
                .As<IAuthenticationService>();

            builder
                .RegisterType<UsersRepository>()
                .As<IUsersRepository>();
            
            builder
               .RegisterType<TokenService>()
               .As<ITokenService>();

            builder
               .RegisterType<TokensRepository>()
               .As<ITokensRepository>();

            builder
               .RegisterType<RedisProvider>()
               .As<IRedisProvider>()
               .SingleInstance();            

            //builder
            //   .RegisterType<FakeDataGenerator>()
            //   .As<IFakeDataGenerator>();

        }
    }
}
