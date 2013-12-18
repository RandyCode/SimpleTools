using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace SilverlightTest.Web
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“EmployeesService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 EmployeesService.svc 或 EmployeesService.svc.cs，然后开始调试。
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmployeesService : IEmployeesService
    {
        public void DoWork()
        {
        }


        public List<EmployeeInfo.Employee> GetEmployeesList()
        {
            return EmployeeInfo.Employee.DefaultEmployees;
        }



        public List<EmployeeInfo.Department> GetDepartmentList()
        {
            return EmployeeInfo.Department.DefaultDepartments;
        }


        public List<EmployeeInfo.Skill> GetSkillList()
        {
            return EmployeeInfo.Skill.DefaultSkills;
        }



        public bool AddEmployee(EmployeeInfo.Employee emp)
        {
            if (emp == null)
            {
                return false;
            }
            else
            {
                EmployeeInfo.Employee.DefaultEmployees.Add(emp);
                return true;
            }
        }

        public bool DeleteEmployees(params int[] empsId)
        {
            if (empsId == null)
            {
                return false;
            }
            else
            {
                foreach (int id in empsId)
                {
                    for (int i = 0; i < EmployeeInfo.Employee.DefaultEmployees.Count; i++)
                    {
                        if (EmployeeInfo.Employee.DefaultEmployees[i].Id == id)
                        {
                            EmployeeInfo.Employee.DefaultEmployees.Remove(EmployeeInfo.Employee.DefaultEmployees[i]);
                            break;
                        }
                    }
                }
                return true;
            }
        }

        public bool ModifyEmployees(EmployeeInfo.Employee emp)
        {        
           if (emp == null)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < EmployeeInfo.Employee.DefaultEmployees.Count; i++)
                {
                    if (EmployeeInfo.Employee.DefaultEmployees[i].Id == emp.Id)
                    {
                        EmployeeInfo.Employee.DefaultEmployees[i] = emp;
                    }
                }
                return true;
            }
        }
    }
}
