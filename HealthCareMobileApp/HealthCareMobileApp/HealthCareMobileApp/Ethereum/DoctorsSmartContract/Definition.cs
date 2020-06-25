using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
namespace HealthCareMobileApp.Ethereum.DoctorsSmartContract
{
    class Definition
    {

        public partial class CheckFunction : CheckFunctionBase { }

        [Function("check", "bool")]
        public class CheckFunctionBase : FunctionMessage
        {
            [Parameter("address", "doc", 1)]
            public virtual string Doc { get; set; }
        }

        public partial class CheckOutputDTO : CheckOutputDTOBase { }

        [FunctionOutput]
        public class CheckOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("bool", "", 1)]
            public virtual bool ReturnValue1 { get; set; }
        }
    }
}
