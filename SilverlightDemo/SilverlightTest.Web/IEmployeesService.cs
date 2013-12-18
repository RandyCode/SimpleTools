using System.Collections.Generic;
using System.ServiceModel;

namespace SilverlightTest.Web
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IEmployeesService”。
    [ServiceContract]
    public interface IEmployeesService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<EmployeeInfo.Employee> GetEmployeesList();

        [OperationContract]
        List<EmployeeInfo.Department> GetDepartmentList();

        [OperationContract]
        List<EmployeeInfo.Skill> GetSkillList();

        [OperationContract]
        bool AddEmployee(EmployeeInfo.Employee emp);

        [OperationContract]
         bool DeleteEmployees(params int[] empsId);

        [OperationContract]
        bool ModifyEmployees(EmployeeInfo.Employee emp);
    }
}
