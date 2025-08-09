using Platform.Repository.Base;

namespace SchoolAPI
{
    public static class ConfigHelper
    {
        public static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            ConfigMirror.ConnectionString = config.GetConnectionString("FB.Service.Con");
            ConfigMirror.ProviderName = config["DatabaseProvider"];
        }
    }
}