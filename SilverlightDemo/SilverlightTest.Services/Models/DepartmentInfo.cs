using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SilverlightTest.Services.EmployeesService;
using System.Collections.ObjectModel;
using Matrix.Silverlight.Net;
using SilverlightTest.Services.Help;

namespace SilverlightTest.Models
{
    public class DepartmentInfo : Department
    {
        public static ObservableCollection<DepartmentInfo> GetDepartment()
        {
            var client = EmployeeClient.CreateEmployeeClient();
            client.GetDepartmentListAsync();
            var result = (ObservableCollection<Services.EmployeesService.Department>)client.WaitForComplete();
            ObservableCollection<DepartmentInfo> list = new ObservableCollection<DepartmentInfo>();
            foreach (var item in result)
            {
                list.Add(ObjectUtils.CloneEx<DepartmentInfo>(item));
            }
            return list;
        }
    }
}
