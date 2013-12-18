
using System.Collections.ObjectModel;
using SilverlightTest.Services.EmployeesService;
using Matrix.Silverlight.Net;
using System.Collections.Generic;
using SilverlightTest.Services.Help;

namespace SilverlightTest.Models
{
    public class EmployeeInfo : Employee
    {
        public EmployeeInfo()
        {
            Skills = new ObservableCollection<SkillInfo>();
        }

        private ObservableCollection<SkillInfo> _skills;

        public new ObservableCollection<SkillInfo> Skills
        {
            get { return _skills; }
            set
            {
                if (_skills == value)
                    return;

                _skills = value;
                RaisePropertyChanged("Skills");
            }
        }

        public static ObservableCollection<EmployeeInfo> GetEmployees()
        {

            EmployeesServiceClient client = EmployeeClient.CreateEmployeeClient();
            client.GetEmployeesListAsync();
            var result = (ObservableCollection<Services.EmployeesService.Employee>)client.WaitForComplete();
            if (result == null)
                return new ObservableCollection<EmployeeInfo>();

            ObservableCollection<EmployeeInfo> list = new ObservableCollection<EmployeeInfo>();
            foreach (var item in result)
            {
                list.Add(CloneFromEmployee(item));
            }
            return list;

        }

   


        public static bool Delete(ObservableCollection<int> list)
        {

            ObservableCollection<int> deleteList = new ObservableCollection<int>();
            foreach (var i in list)
            {
                deleteList.Add(i);
            }
            var client = EmployeeClient.CreateEmployeeClient();
            client.DeleteEmployeesAsync(deleteList);
            bool res = (bool)client.WaitForComplete();
            return res;
        }


        public static void Add(EmployeeInfo emp)
        {
            var client = EmployeeClient.CreateEmployeeClient();
            client.AddEmployeeAsync(ConvertToEmployee(emp));
            client.WaitForComplete();
        }

        public static void Modify(EmployeeInfo emp)
        {
            var client = EmployeeClient.CreateEmployeeClient();
            client.ModifyEmployeesAsync(ConvertToEmployee(emp));
            client.WaitForComplete();
        }

        public static bool CheckName(string name)
        {
            ObservableCollection<EmployeeInfo> emps = GetEmployees();
            foreach (EmployeeInfo emp in emps)
            {
                if (emp.Name == name)
                    return false;
            }
            return true;
        }

        private static EmployeeInfo CloneFromEmployee(Employee employee)
        {
            EmployeeInfo result = new EmployeeInfo();
            result.Id = employee.Id;
            result.Birthday = employee.Birthday;
            result.Bonus = employee.Bonus;
            result.DepartmentId = employee.DepartmentId;
            result.DepartmentName = employee.DepartmentName;
            result.Level = employee.Level;
            result.Name = employee.Name;
            result.Remark = employee.Remark;
            result.Salary = employee.Salary;
            result.Sex = employee.Sex;

            result.Skills = new ObservableCollection<SkillInfo>();

            foreach (var item in employee.Skills)
            {
                result.Skills.Add(ObjectUtils.CloneEx<SkillInfo>(item));
            }
            return result;
        }

        private static Employee ConvertToEmployee(EmployeeInfo emp)
        {
            Employee result = new Employee();
            result.Id = emp.Id;
            result.Birthday = emp.Birthday;
            result.Bonus = emp.Bonus;
            result.DepartmentId = emp.DepartmentId;
            result.DepartmentName = emp.DepartmentName;
            result.Level = emp.Level;
            result.Name = emp.Name;
            result.Remark = emp.Remark;
            result.Salary = emp.Salary;
            result.Sex = emp.Sex;
            result.Skills = new ObservableCollection<Skill>();
            foreach (var item in emp.Skills)
            {
                result.Skills.Add(ObjectUtils.CloneEx<Skill>(item));
            }
            return result;
        }

    }
}
