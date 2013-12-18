using Matrix.Silverlight.Net;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SilverlightTest
{
    public class UserClient
    {

        private static SilverlightTest.Services.UserService.UserServiceClient CreateClient()
        {
           
            SilverlightTest.Services.UserService.UserServiceClient client = ServiceClientExtension.CreateServiceClient<SilverlightTest.Services.UserService.UserServiceClient>(Services.ServiceInfo.UserServiceUri, false);  //true 自定义绑定  false 默认
            return client;
        }

        public static bool Login(string username, string password)
        {
            var client = CreateClient();
            client.LoginAsync(username, password);
            var user = (SilverlightTest.Services.UserService.ValidateType)client.WaitForComplete();
            return user == SilverlightTest.Services.UserService.ValidateType.Successed;
        }

        public static ObservableCollection<string> GetOperationCodes(string username)
        {
            var client = CreateClient();
            client.GetOperationCodesAsync(username);
            var list = (ObservableCollection<string>)client.WaitForComplete();
            return list;
        }
    } 

}
