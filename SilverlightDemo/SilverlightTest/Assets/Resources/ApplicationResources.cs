using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightTest.Assets.Resources
{
    public class ApplicationResources :INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private static List<ApplicationResources> _resources = new List<ApplicationResources>();
        private static LoginResources _LoginResources = new LoginResources();

        public LoginResources LanguageResources
        {  
            get { return _LoginResources; }
        }
 

        public ApplicationResources()
        {
            _resources.Add(this);
        }

        public void RaisePropertyChanged()
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(""));
        }

        public static void ChangeCulture(System.Globalization.CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;

            //----------2013/8/12 15:30  区域语言切换
            LoginResources.Culture = cultureInfo;

            for (int i = 0; i < _resources.Count; i++) 
            {
                _resources[i].RaisePropertyChanged();
            }

           
 

        }

    }
}
