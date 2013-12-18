using System;
using System.Net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace SilverlightTest.Exceptions
{
    public class WcfFault : object, System.ComponentModel.INotifyPropertyChanged
    {
        public static bool IsWcfFault(Exception ex)
        {
            FaultException<ExceptionDetail> fault = FindFaultException(ex);
            if (fault != null)
            {
                if (string.IsNullOrEmpty(fault.Message))
                    return false;
                else
                    GetWcfFaultMsg = fault.Detail.Message;
                return true;
            }
            return false;
        }

        private static FaultException<ExceptionDetail> FindFaultException(Exception ex)
        {
            Exception tmp = ex;
            while (tmp != null)
            {
                if (tmp is FaultException<ExceptionDetail>)
                    return tmp as FaultException<ExceptionDetail>;
                tmp = tmp.InnerException;
            }
            return null;
        }

        private static string _faultmsg;
        public static string GetWcfFaultMsg
        {
            get
            {
                return _faultmsg;
            }
            set
            {
                _faultmsg = value;
            }
        }




        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}
