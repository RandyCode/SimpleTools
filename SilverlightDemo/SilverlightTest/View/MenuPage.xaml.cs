using Matrix.Silverlight.Forms;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace SilverlightTest   //命名空间没改成.View
{
    public partial class Menu : UserControl
    {
        public Menu(string name)
        {
            InvokeUtils.ThreadProc(this, () =>
            {
                WaittingDialog.Show(this);
                var list = SilverlightTest.UserClient.GetOperationCodes(name);
                InvokeUtils.SyncInvoke(this, () => {                   
                    Helper.SystemInfo.OperationCodes = list;
                    InitializeComponent();
                    var item = MenuList.Items[0] as ListBoxItem;
                    Helper.Helper.ShowNavigationDetail(item, Detail);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-Hans");
                    Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
                });

            });
   
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            HtmlPage.Window.Eval("window.location.reload()");
        }



        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0] as ListBoxItem;
            Helper.Helper.ShowNavigationDetail(item, Detail);
        }
    }
}
