using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace SilverlightTest.Web.EmployeeInfo
{
    [DataContract]
    public class Department
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        private static List<Department> _defaultDepartments = new List<Department>();

        public static List<Department> DefaultDepartments
        {
            get { return Department._defaultDepartments; }
        }

        static Department()
        {
            _defaultDepartments.Add(new Department { Id = 0, Name = "C# Department" });
            _defaultDepartments.Add(new Department { Id = 1, Name = "JAVA Department" });
            _defaultDepartments.Add(new Department { Id = 2, Name = "Manage Office" });
        }
    }
}