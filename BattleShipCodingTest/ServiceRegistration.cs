
using BattleShipCodingTest.Shared.Interface;
using BattleShipCodingTest.Shared.Service;
using Microsoft.Extensions.DependencyInjection;

namespace BattleShipCodingTest
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services) //, IConfiguration configuration
        {
            services.AddSingleton<IBattleShipService, BattleShipService>();           
        }
    }
}
