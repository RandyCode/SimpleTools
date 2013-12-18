using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightTest.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException()
        {
            ExceptionMessage = ExceptionMessage == "" ? base.Message : ExceptionMessage;
        }
        public string Code { get; set; }
        public string ExceptionMessage { get; set; }
        public override string Message
        {
            get
            {
                return this.ExceptionMessage;
            }
        }
    }
}
