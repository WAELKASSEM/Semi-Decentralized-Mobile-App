using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace HealthCareMobileApp.Ethereum
{
    sealed class Config
    {
        private static Config config;
        private Config(Account account)
        {
            Web3Instance = new Web3(account,URL);
        }
        public Web3 Web3Instance { get; }
        private readonly string URL;
        public static void Init(Account account)
        {
            if (config != default(Config)) return;
            config = new Config(account);
        }
        public static Config Instance() => config;
    }
}
