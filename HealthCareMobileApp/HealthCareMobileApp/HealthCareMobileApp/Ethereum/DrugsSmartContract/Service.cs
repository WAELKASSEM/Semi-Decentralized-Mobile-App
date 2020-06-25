using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using System.Threading.Tasks;
using static HealthCareMobileApp.Ethereum.DrugsSmartContract.Definition;

namespace HealthCareMobileApp.Ethereum.DrugsSmartContract
{
    class Service
    {
        protected Nethereum.Web3.Web3 Web3 { get; }
        private readonly string ContractAddress;

        public ContractHandler ContractHandler { get; }

        public Service(Nethereum.Web3.Web3 web3)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(ContractAddress);
        }

        public Task<ulong> GetQueryAsync(GetFunction getFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFunction, ulong>(getFunction, blockParameter);
        }

        public Task<ulong> GetQueryAsync(BigInteger code, BlockParameter blockParameter = null)
        {
            var getFunction = new GetFunction();
            getFunction.Code = code;

            return ContractHandler.QueryAsync<GetFunction, ulong>(getFunction, blockParameter);
        }

    }
}
