using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using static HealthCareMobileApp.Ethereum.RelationshipManagerSmartContract.Definition;

namespace HealthCareMobileApp.Ethereum.RelationshipManagerSmartContract
{
    class Service
    {
        protected Nethereum.Web3.Web3 Web3 { get; }

        public ContractHandler ContractHandler { get; }

        private readonly string ContractAddress;

        public Service(Nethereum.Web3.Web3 web3)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(ContractAddress);
        }

        public Task<string> AddRequestAsync(AddFunction addFunction)
        {
            return ContractHandler.SendRequestAsync(addFunction);
        }

        public Task<TransactionReceipt> AddRequestAndWaitForReceiptAsync(AddFunction addFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(addFunction, cancellationToken);
        }

        public Task<string> AddRequestAsync(string doc)
        {
            var addFunction = new AddFunction();
            addFunction.Doc = doc;
            return ContractHandler.SendRequestAsync(addFunction);
        }

        public Task<TransactionReceipt> AddRequestAndWaitForReceiptAsync(string doc, CancellationTokenSource cancellationToken = null)
        {
            var addFunction = new AddFunction();
            addFunction.Doc = doc;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(addFunction, cancellationToken);
        }

        public Task<bool> CheckDoctorQueueQueryAsync(CheckDoctorQueueFunction checkDoctorQueueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CheckDoctorQueueFunction, bool>(checkDoctorQueueFunction, blockParameter);
        }


        public Task<bool> CheckDoctorQueueQueryAsync(string doc, BlockParameter blockParameter = null)
        {
            var checkDoctorQueueFunction = new CheckDoctorQueueFunction();
            checkDoctorQueueFunction.Doc = doc;

            return ContractHandler.QueryAsync<CheckDoctorQueueFunction, bool>(checkDoctorQueueFunction, blockParameter);
        }

        public Task<bool> CheckDuplicateRequestQueryAsync(CheckDuplicateRequestFunction checkDuplicateRequestFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CheckDuplicateRequestFunction, bool>(checkDuplicateRequestFunction, blockParameter);
        }


        public Task<bool> CheckDuplicateRequestQueryAsync(string doc, BlockParameter blockParameter = null)
        {
            var checkDuplicateRequestFunction = new CheckDuplicateRequestFunction();
            checkDuplicateRequestFunction.Doc = doc;
            return ContractHandler.QueryAsync<CheckDuplicateRequestFunction, bool>(checkDuplicateRequestFunction, blockParameter);
        }

        public Task<bool> CheckPossibilityQueryAsync(CheckPossibilityFunction checkPossibilityFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CheckPossibilityFunction, bool>(checkPossibilityFunction, blockParameter);
        }


        public Task<bool> CheckPossibilityQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CheckPossibilityFunction, bool>(null, blockParameter);
        }

        public Task<string> GetDocQueryAsync(GetDocFunction getDocFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDocFunction, string>(getDocFunction, blockParameter);
        }


        public Task<string> GetDocQueryAsync(BigInteger index, BlockParameter blockParameter = null)
        {
            var getDocFunction = new GetDocFunction();
            getDocFunction.Index = index;

            return ContractHandler.QueryAsync<GetDocFunction, string>(getDocFunction, blockParameter);
        }

        public Task<GetPatientOutputDTO> GetPatientQueryAsync(GetPatientFunction getPatientFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPatientFunction, GetPatientOutputDTO>(getPatientFunction, blockParameter);
        }

        public Task<GetPatientOutputDTO> GetPatientQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPatientFunction, GetPatientOutputDTO>(null, blockParameter);
        }

        public Task<string> RespondRequestAsync(RespondFunction respondFunction)
        {
            return ContractHandler.SendRequestAsync(respondFunction);
        }

        public Task<TransactionReceipt> RespondRequestAndWaitForReceiptAsync(RespondFunction respondFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(respondFunction, cancellationToken);
        }

        public Task<string> RespondRequestAsync(string patient, BigInteger status)
        {
            var respondFunction = new RespondFunction();
            respondFunction.Patient = patient;
            respondFunction.Status = status;

            return ContractHandler.SendRequestAsync(respondFunction);
        }

        public Task<TransactionReceipt> RespondRequestAndWaitForReceiptAsync(string patient, BigInteger status, CancellationTokenSource cancellationToken = null)
        {
            var respondFunction = new RespondFunction();
            respondFunction.Patient = patient;
            respondFunction.Status = status;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(respondFunction, cancellationToken);
        }

        public Task<string> UpdatePatientRequestAsync(UpdatePatientFunction updatePatientFunction)
        {
            return ContractHandler.SendRequestAsync(updatePatientFunction);
        }

        public Task<string> UpdatePatientRequestAsync()
        {
            return ContractHandler.SendRequestAsync<UpdatePatientFunction>();
        }

        public Task<TransactionReceipt> UpdatePatientRequestAndWaitForReceiptAsync(UpdatePatientFunction updatePatientFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(updatePatientFunction, cancellationToken);
        }

        public Task<TransactionReceipt> UpdatePatientRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync<UpdatePatientFunction>(null, cancellationToken);
        }
    }
}
