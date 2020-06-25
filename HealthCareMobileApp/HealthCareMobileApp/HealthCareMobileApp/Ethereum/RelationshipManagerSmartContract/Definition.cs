using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace HealthCareMobileApp.Ethereum.RelationshipManagerSmartContract
{
    class Definition
    {

        public partial class AddFunction : AddFunctionBase { }

        [Function("add")]
        public class AddFunctionBase : FunctionMessage
        {
            [Parameter("address", "doc", 1)]
            public virtual string Doc { get; set; }
        }

        public partial class CheckDoctorQueueFunction : CheckDoctorQueueFunctionBase { }

        [Function("checkDoctorQueue", "bool")]
        public class CheckDoctorQueueFunctionBase : FunctionMessage
        {
            [Parameter("address", "doc", 1)]
            public virtual string Doc { get; set; }
        }

        public partial class CheckDuplicateRequestFunction : CheckDuplicateRequestFunctionBase { }

        [Function("checkDuplicateRequest", "bool")]
        public class CheckDuplicateRequestFunctionBase : FunctionMessage
        {
            [Parameter("address", "doc", 1)]
            public virtual string Doc { get; set; }
        }

        public partial class CheckPossibilityFunction : CheckPossibilityFunctionBase { }

        [Function("checkPossibility", "bool")]
        public class CheckPossibilityFunctionBase : FunctionMessage
        {

        }

        public partial class GetDocFunction : GetDocFunctionBase { }

        [Function("getDoc", "address")]
        public class GetDocFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "index", 1)]
            public virtual BigInteger Index { get; set; }
        }

        public partial class GetPatientFunction : GetPatientFunctionBase { }

        [Function("getPatient", typeof(GetPatientOutputDTO))]
        public class GetPatientFunctionBase : FunctionMessage
        {

        }

        public partial class RespondFunction : RespondFunctionBase { }

        [Function("respond")]
        public class RespondFunctionBase : FunctionMessage
        {
            [Parameter("address", "patient", 1)]
            public virtual string Patient { get; set; }
            [Parameter("uint256", "status", 2)]
            public virtual BigInteger Status { get; set; }
        }

        public partial class UpdatePatientFunction : UpdatePatientFunctionBase { }

        [Function("updatePatient")]
        public class UpdatePatientFunctionBase : FunctionMessage
        {

        }



        public partial class CheckDoctorQueueOutputDTO : CheckDoctorQueueOutputDTOBase { }

        [FunctionOutput]
        public class CheckDoctorQueueOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("bool", "", 1)]
            public virtual bool ReturnValue1 { get; set; }
        }

        public partial class CheckDuplicateRequestOutputDTO : CheckDuplicateRequestOutputDTOBase { }

        [FunctionOutput]
        public class CheckDuplicateRequestOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("bool", "", 1)]
            public virtual bool ReturnValue1 { get; set; }
        }

        public partial class CheckPossibilityOutputDTO : CheckPossibilityOutputDTOBase { }

        [FunctionOutput]
        public class CheckPossibilityOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("bool", "", 1)]
            public virtual bool ReturnValue1 { get; set; }
        }

        public partial class GetDocOutputDTO : GetDocOutputDTOBase { }

        [FunctionOutput]
        public class GetDocOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }

        public partial class GetPatientOutputDTO : GetPatientOutputDTOBase { }

        [FunctionOutput]
        public class GetPatientOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
            [Parameter("uint256", "", 2)]
            public virtual BigInteger ReturnValue2 { get; set; }
        }



    }
}
