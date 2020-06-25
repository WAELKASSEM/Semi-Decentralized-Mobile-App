using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace HealthCareMobileApp.Ethereum.DrugsSmartContract
{
    class Definition
    {
        public partial class AddFunction : AddFunctionBase { }

        [Function("add")]
        public class AddFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "code", 1)]
            public virtual BigInteger Code { get; set; }
            [Parameter("uint64", "price_in_gwei", 2)]
            public virtual ulong Price_in_gwei { get; set; }
        }

        public partial class Change_creatorFunction : Change_creatorFunctionBase { }

        [Function("change_creator")]
        public class Change_creatorFunctionBase : FunctionMessage
        {
            [Parameter("address", "new_creator", 1)]
            public virtual string New_creator { get; set; }
        }

        public partial class GetFunction : GetFunctionBase { }

        [Function("get", "uint64")]
        public class GetFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "code", 1)]
            public virtual BigInteger Code { get; set; }
        }





        public partial class GetOutputDTO : GetOutputDTOBase { }

        [FunctionOutput]
        public class GetOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint64", "", 1)]
            public virtual ulong ReturnValue1 { get; set; }
        }
    }
}
