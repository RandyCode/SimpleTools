
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace SilverlightTest.Web
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserService”。
    [ServiceContract]
    public interface IUserService
    {

        [OperationContract]
        [FaultContract(typeof(ExceptionDetail))]
        ValidateType Login(string userName, string password);

        [OperationContract]
        IList<string> GetOperationCodes(string userName);
    }

    public enum ValidateType
    {
        Successed = 0,
        Failed = 1,
        Other = 2
    }
}
