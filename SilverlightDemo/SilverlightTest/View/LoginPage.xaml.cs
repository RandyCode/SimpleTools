
using Matrix.Silverlight.Forms;
using Matrix.Silverlight.Validation;
using SilverlightTest.Assets.Resources;
using SilverlightTest.Exceptions;
using SilverlightTest.Helper;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace SilverlightTest
{
    public partial class Login : UserControl
    {
        private Model.UserModel _model;

        public Login()
        {
            InitializeComponent();
            InitLanguageBox();
            _model = new Model.UserModel();
            if (System.Diagnostics.Debugger.IsAttached)
            {
                _model.Password = "admin";
                _model.Username = "admin";
            }
            this.DataContext = _model;
        }


        private void OnLogin(object sender, RoutedEventArgs e)
        {
            //触发验证
            ValidationToolkit toolkit = new ValidationToolkit(this);
            if (toolkit.Validate() == false)
            {
                toolkit.ReportError(this);
                return;
            }

            InvokeUtils.ThreadProc(this, () =>
            {
                WaittingDialog.Show(this);
                if (SilverlightTest.UserClient.Login(_model.Username, _model.Password) == false)
                {

                    MsgBox.Error(this, "用户名，密码错误");
                    return;
                }

                InvokeUtils.SyncInvoke(this, () =>
                {   
                     this.Content = new Menu(_model.Username);        
                });
            });

        }


        private void InitLanguageBox()
        {
            LanguageComboBox.SelectionChanged += OnChangeLanguage;    
            LanguageComboBox.ItemsSource = LanguageInfo.Languages;
            LanguageComboBox.SelectedItem = LanguageInfo.CurrentLanguage;
   
       
        }

        private void OnChangeLanguage(object sender, SelectionChangedEventArgs e)
        {
            LanguageInfo lang = LanguageComboBox.SelectedItem as LanguageInfo;
            if (lang == null)
                return;

            ApplicationResources.ChangeCulture(new System.Globalization.CultureInfo(lang.Name));
            LanguageInfo.CurrentLanguage = lang;
            DateTimeFormatUtil.ChangeCurrentDateTimeFormat(); 

      
        }


    }
}
