using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SilverlightTest.Web.EmployeeInfo
{
    [DataContract]
    public class Skill
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Info { get; set; }

        private static List<Skill> _defaultSkills = new List<Skill>();

        public static List<Skill> DefaultSkills
        {
            get { return Skill._defaultSkills; }
        }

        static Skill()
        {
            _defaultSkills.Add(new Skill { Id = 0, Info = "C#" });
            _defaultSkills.Add(new Skill { Id = 1, Info = "MS SQL" });
            _defaultSkills.Add(new Skill { Id = 2, Info = "ASP.NET" });
            _defaultSkills.Add(new Skill { Id = 3, Info = "WPF" });
            _defaultSkills.Add(new Skill { Id = 4, Info = "WCF" });
            _defaultSkills.Add(new Skill { Id = 5, Info = "Silverlight" });
            _defaultSkills.Add(new Skill { Id = 6, Info = "HTML" });
            _defaultSkills.Add(new Skill { Id = 7, Info = "CSS" });
            _defaultSkills.Add(new Skill { Id = 8, Info = "UML" });
        }
    }
}