using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace SilverlightTest.Web.EmployeeInfo
{
    [DataContract]
    public class Employee
    {


        private int id;
        [DataMember]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        private string _name;

        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        private int _departmentId;

        [DataMember]
        public int DepartmentId
        {
            get { return _departmentId; }
            set
            {
                if (_departmentId == value)
                    return;

                _departmentId = value;
            }
        }

        private string _departmentName;

        [DataMember]
        public string DepartmentName
        {
            get { return _departmentName; }
            set
            {
                if (_departmentName == value)
                    return;

                _departmentName = value;
            }
        }

        private DateTime _birthday;

        [DataMember]
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                if (_birthday == value)
                    return;

                _birthday = value;
            }
        }

        private double _salary;

        [DataMember]
        public double Salary
        {
            get { return _salary; }
            set
            {
                if (_salary == value)
                    return;

                _salary = value;
            }
        }

        private double? _bonus;

        [DataMember]
        public double? Bonus
        {
            get { return _bonus; }
            set
            {
                if (_bonus == value)
                    return;

                _bonus = value;
            }
        }

        private string _remark;

        [DataMember]
        public string Remark
        {
            get { return _remark; }
            set
            {
                if (_remark == value)
                    return;
                _remark = value;
            }
        }

        private bool _sex;

        [DataMember]
        public bool Sex
        {
            get { return _sex; }
            set
            {
                if (_sex == value)
                    return;

                _sex = value;

            }
        }

        private int _level;

        [DataMember]
        public int Level
        {
            get { return _level; }
            set
            {
                if (_level == value)
                    return;

                _level = value;
            }
        }

        private List<Skill> _skills;

        [DataMember]
        public List<Skill> Skills
        {
            get { return _skills; }
            set
            {
                if (_skills == value)
                    return;
                _skills = value;
            }
        }

        private static List<Employee> _defaultEmployees = new List<Employee>();

        public static List<Employee> DefaultEmployees
        {
            get { return Employee._defaultEmployees; }
            set { Employee._defaultEmployees = value; }
        }


        static Employee()
        {
            _defaultEmployees.Add(new Employee()
            {
                Id = 1,
                Name = "Jason",
                Birthday = new DateTime(1980, 1, 2, 0, 0, 0),
                DepartmentId = Department.DefaultDepartments[0].Id,
                DepartmentName = Department.DefaultDepartments[0].Name,
                Bonus = 1000,
                Salary = 12345.67,
                Sex = true,
                Level = 1,
                Remark = "The .zip file contains a sample Employee model deployment package. The sample includes examples of recursive derived hierarchies.",
                Skills = new List<Skill> { Skill.DefaultSkills[0], Skill.DefaultSkills[1], Skill.DefaultSkills[2], Skill.DefaultSkills[3], Skill.DefaultSkills[4] }
            });

            _defaultEmployees.Add(new Employee()
            {
                Id = 2,
                Name = "Alison",
                Birthday = new DateTime(1982, 3, 2, 0, 0, 0),
                DepartmentId = Department.DefaultDepartments[0].Id,
                DepartmentName = Department.DefaultDepartments[0].Name,
                Bonus = null,
                Salary = 12345.67,
                Sex = true,
                Level = 1,
                Remark = "Smartphones cost far less than their suggested prices thanks to carrier subsidization. That two-year contract you lock yourself into every time you pay $200 for a fancy phone?",
                Skills = new List<Skill> { Skill.DefaultSkills[0], Skill.DefaultSkills[2], Skill.DefaultSkills[3], Skill.DefaultSkills[4] }
            });

            _defaultEmployees.Add(new Employee()
            {
                Id = 3,
                Name = "James",
                Birthday = new DateTime(1982, 10, 2, 0, 0, 0),
                DepartmentId = Department.DefaultDepartments[0].Id,
                DepartmentName = Department.DefaultDepartments[0].Name,
                Bonus = null,
                Salary = 1000000,
                Sex = false,
                Level = 2,
                Remark = "For reference, here’s what Apple sells “unlocked” – meaning you’d buy the phone without agreeing to a two-year service contract,",
                Skills = new List<Skill> { Skill.DefaultSkills[0], Skill.DefaultSkills[3], Skill.DefaultSkills[4] }
            });

            _defaultEmployees.Add(new Employee()
            {
                Id = 4,
                Name = "Celia",
                Birthday = new DateTime(1982, 6, 2, 0, 0, 0),
                DepartmentId = Department.DefaultDepartments[1].Id,
                DepartmentName = Department.DefaultDepartments[1].Name,
                Bonus = 100000,
                Salary = 500,
                Sex = false,
                Level = 3,
                Remark = "forgoing the subsidized handset price in the process — iPhones for right now: 8GB iPhone 4 for $549, 16GB iPhone 4S for $649, 32GB iPhone 4S for $749 and the 64GB iPhone 4S for $849.",
                Skills = new List<Skill> { Skill.DefaultSkills[0], Skill.DefaultSkills[1], Skill.DefaultSkills[2], Skill.DefaultSkills[3], Skill.DefaultSkills[4], Skill.DefaultSkills[5], Skill.DefaultSkills[6], Skill.DefaultSkills[7], Skill.DefaultSkills[8] }
            });

            _defaultEmployees.Add(new Employee()
            {
                Id = 5,
                Name = "Robert",
                Birthday = new DateTime(1988, 10, 2, 0, 0, 0),
                DepartmentId = Department.DefaultDepartments[2].Id,
                DepartmentName = Department.DefaultDepartments[2].Name,
                Bonus = null,
                Salary = 10005,
                Sex = true,
                Level = 1,
                Remark = "That’s why it’s important to not lose or damage your phone. Even though you paid $200 for it, it costs a lot more than $200 to replace if you’re not up for a contract renewal.",
                Skills = new List<Skill> { Skill.DefaultSkills[0], Skill.DefaultSkills[3], Skill.DefaultSkills[4] }
            });

            _defaultEmployees.Add(new Employee()
            {
                Id = 6,
                Name = "Linda",
                Birthday = new DateTime(1968, 10, 2, 0, 0, 0),
                DepartmentId = Department.DefaultDepartments[1].Id,
                DepartmentName = Department.DefaultDepartments[1].Name,
                Bonus = 90,
                Salary = 80,
                Sex = true,
                Level = 1,
                Remark = "Aside from all that, this $800 rumor is pure speculation. If there’s a grain of truth to it, $800 is almost certainly the price of one of the models of phones when purchased without a contract.",
                Skills = new List<Skill> { Skill.DefaultSkills[0], }
            });

            _defaultEmployees.Add(new Employee()
            {
                Id = 7,
                Name = "David",
                Birthday = new DateTime(1969, 10, 2, 0, 0, 0),
                DepartmentId = Department.DefaultDepartments[1].Id,
                DepartmentName = Department.DefaultDepartments[1].Name,
                Bonus = null,
                Salary = 80,
                Sex = true,
                Level = 2,
                Remark = "If any carrier tried to sell the new iPhone for $800 with a two-year contract, nobody would buy it.",
                Skills = new List<Skill> { }
            });


            _defaultEmployees.Add(new Employee()
            {
                Id = 8,
                Name = "James",
                Birthday = new DateTime(1978, 9, 3, 0, 0, 0),
                DepartmentId = Department.DefaultDepartments[1].Id,
                DepartmentName = Department.DefaultDepartments[1].Name,
                Bonus = null,
                Salary = 8,
                Sex = true,
                Level = 2,
                Remark = "Actually, don’t quote me on that. You never know with Apple stuff.",
                Skills = new List<Skill> { Skill.DefaultSkills[4], Skill.DefaultSkills[3], Skill.DefaultSkills[7] }
            });

            _defaultEmployees.Add(new Employee()
            {
                Id = 9,
                Name = "Joan",
                Birthday = new DateTime(1988, 6, 23, 0, 0, 0),
                DepartmentId = Department.DefaultDepartments[1].Id,
                DepartmentName = Department.DefaultDepartments[1].Name,
                Bonus = 20.22,
                Salary = 88.87,
                Sex = true,
                Level = 2,
                Remark = "The new iPhone – whenever it comes out and whatever features it’s got – will be priced in line with other smartphones sold via wireless carriers here in the U.S., though. That’s a given.",
                Skills = new List<Skill> { Skill.DefaultSkills[7] }
            });
        }




    }
}