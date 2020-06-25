using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using System.Threading.Tasks;
using static HealthCareMobileApp.Ethereum.DoctorsSmartContract.Definition;

namespace HealthCareMobileApp.Ethereum.DoctorsSmartContract
{
    class Service
    {
        protected Nethereum.Web3.Web3 Web3 { get; }
        private readonly string ContractAddress ;

        public ContractHandler ContractHandler { get; }

        public Service(Nethereum.Web3.Web3 web3)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(ContractAddress);
        }
        public Task<bool> CheckQueryAsync(CheckFunction checkFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CheckFunction, bool>(checkFunction, blockParameter);
        }


        public Task<bool> CheckQueryAsync(string doc, BlockParameter blockParameter = null)
        {
            var checkFunction = new CheckFunction();
            checkFunction.Doc = doc;

            return ContractHandler.QueryAsync<CheckFunction, bool>(checkFunction, blockParameter);
        }
    }
}
