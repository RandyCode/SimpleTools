
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
namespace SilverlightTest.Web
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 UserService.svc 或 UserService.svc.cs，然后开始调试。
    public class UserService : IUserService
    {
        public ValidateType Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return ValidateType.Failed;

            if (userName.ToUpper() == "USER" || userName.ToUpper() == "ADMIN" && password.ToUpper() == "ADMIN")
            {
                return ValidateType.Successed;
            }
            else
            {
                var d = new FaultException<ExceptionDetail>(new ExceptionDetail(new Exception())
                {
                     Message= "WCF:帐号密码,错误"
                });
                
                throw d;
            
            }
        }


        public IList<string> GetOperationCodes(string userName)
        {
            List<string> list = new List<string>();
            if (userName.ToUpper() == "ADMIN")
            {
                list.Add("FILES_OPERATION");
                list.Add("DELETE_OPERATION");
                list.Add("ADD_OPERATION");
            }
            list.Add("EMPLOYEE_OPERATION");
            return list;
        }
    }
}
