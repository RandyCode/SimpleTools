using Matrix.Silverlight.Net;
using System.Collections.ObjectModel;
using SilverlightTest.Services.EmployeesService;

namespace SilverlightTest
{
    public class EmployeeClient
    {
        public static EmployeesServiceClient CreateEmployeeClient()
        {
            return ServiceClientExtension.CreateServiceClient<EmployeesServiceClient>(Services.ServiceInfo.EmployeeServiceUri, false);
        }


    }
}
