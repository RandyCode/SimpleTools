using SilverlightTest.Services.EmployeesService;
using System.Collections.ObjectModel;
using Matrix.Silverlight.Net;

namespace SilverlightTest.Models
{
    public class SkillInfo:Skill
    {

        public static ObservableCollection<SkillInfo> GetSKills()
        {
        
            var client = EmployeeClient.CreateEmployeeClient();
            client.GetSkillListAsync();
            var result = (ObservableCollection<Skill>)client.WaitForComplete();
            if (result == null)
                return new ObservableCollection<SkillInfo>();

            ObservableCollection<SkillInfo> list = new ObservableCollection<SkillInfo>();
            foreach (var item in result)
            {
                list.Add(SilverlightTest.Services.Help.ObjectUtils.CloneEx<SkillInfo>(item));
            }
            return list;

        }
    }
}
