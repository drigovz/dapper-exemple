using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infra.Data.Helpers
{
    public static class ConnectionStringConfig
    {
        private static IConfiguration currentConfig;

        public static void SetConfig(IConfiguration configuration)
        {
            currentConfig = configuration;
        }

        public static string GetConnection
        {
            get => currentConfig.GetConnectionString("DefaultConnection");
        }
    }
}
