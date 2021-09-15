using Microsoft.Extensions.Configuration;

namespace PLS.Infrastructure.Configurations
{
    public class ConfigFactory : IConfigFactory
    {
        private readonly IConfiguration _configuration;

        public ConfigFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ILevelConfig GetLevelConfig()
        {
            return _configuration.GetSection("LevelConfiguration").Get<LevelConfig>();
        }
    }
}
