using Microsoft.Extensions.DependencyInjection;
using Platform.Business;
using Platform.Repository;

namespace Platform.DependencyInjection
{
    public static class DependencyRegister
    {
        public static IServiceCollection AddPlatformDependencies(this IServiceCollection services)
        {
            // Registre aqui todas as dependências do seu domínio
            services.AddScoped<UserDAO>();
            services.AddScoped<UserBUS>();

            // Adicione outros DAOs, BUSs, serviços, etc. conforme necessário

            return services;
        }
    }
}