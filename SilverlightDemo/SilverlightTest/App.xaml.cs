
using SilverlightTest.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightTest
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Services.ServiceInfo.Init();
            this.RootVisual = new MainPage();
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            //MessageBox.Show(e.ExceptionObject.Message);
            try
            {
                if (RootVisual == null)
                {
                    string msg = e.ExceptionObject.Message;
                    if (e.ExceptionObject is CustomException)
                    {
                        CustomException csEx = (CustomException)e.ExceptionObject;
                        msg = csEx.ExceptionMessage;
                    }
                    System.Windows.MessageBox.Show(msg);
                    return;
                }
                else if (RootVisual.CheckAccess())  //是否能线程访问
                {
                    HandleAppException(e);
                }
                else
                {
                    RootVisual.Dispatcher.BeginInvoke(() =>
                    {
                        HandleAppException(e);
                    });
                }
            }
            catch (Exception)
            {
 
            }
            e.Handled = true;
            return;

        }

        private void HandleAppException(ApplicationUnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is CustomException)
            {
                CustomException csEx = (CustomException)e.ExceptionObject;
                System.Windows.MessageBox.Show(csEx.ExceptionMessage);
                return;
            }
            if (WcfFault.IsWcfFault(e.ExceptionObject))
            {
                System.Windows.MessageBox.Show(WcfFault.GetWcfFaultMsg);
                return;
            }
            System.Windows.MessageBox.Show(e.ExceptionObject.Message);
        }

    
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
