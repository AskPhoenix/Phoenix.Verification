using Microsoft.Extensions.Configuration;

namespace Phoenix.Verification.Base
{
    public abstract class ConfigurationTestsBase
    {
        protected readonly IConfiguration _configuration;

        public ConfigurationTestsBase()
            : base()
        {
            // Attention to the json file path if the derived class is located in a subfolder
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
