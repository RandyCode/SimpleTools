using System.Net;
using System.Net.Browser;
using System.Windows;

namespace SilverlightTest.Services
{
    public class ServiceInfo
    {
        public static void Init()
        {
            bool registerResult = WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
            bool httpsResult = WebRequest.RegisterPrefix("https://", WebRequestCreator.ClientHttp);

            var source = Application.Current.Host.Source;
            var uri = source.AbsoluteUri.Substring(0, source.AbsoluteUri.Length - source.AbsolutePath.Length);
            UserServiceUri = uri + "/" + "UserService.svc";
            EmployeeServiceUri = uri + "/" + "EmployeesService.svc";


        }

        public static string UserServiceUri { get; private set; }
        public static string EmployeeServiceUri { get; private set; }
 
    }
}
