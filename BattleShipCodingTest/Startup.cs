using BattleShipCodingTest.Shared;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BattleShipCodingTest.Startup))]

namespace BattleShipCodingTest
{
    public class Startup : FunctionsStartup
    {               
        public override void Configure(IFunctionsHostBuilder builder)
        {            
            ConfigureServices(builder.Services);            
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddPersistenceInfrastructure();
        }
    }
}

