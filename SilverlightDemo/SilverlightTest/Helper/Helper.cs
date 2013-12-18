using System.Windows.Controls;

namespace SilverlightTest.Helper
{
    public class Helper
    {
        private static UserControl _currentControl=null;

        public static void ShowNavigationDetail(ListBoxItem item,Grid element)
        {
            if (item != null)
            {
                element.Children.Remove(_currentControl);
                EnumMenuType type = (EnumMenuType)System.Convert.ToInt32(item.Tag);
                switch (type)
                {
                    case EnumMenuType.Employees:
                        _currentControl = new View.Employees();
                        element.Children.Add(_currentControl);
                        break;
                    case EnumMenuType.Files:              
                        break;
                    default: break;
                }
     
            }
        }



    }

    public enum EnumMenuType
    {
        Employees,
        Files
    }
}
