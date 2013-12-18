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

namespace SilverlightTest.Helper
{
    public enum OperationAction
    {
        Disable = 1,
        Hide = 2
    }
    public class Operation : DependencyObject
    {
        //--------Action
        public static OperationAction GetAction(DependencyObject obj)
        {
            return (OperationAction)obj.GetValue(ActionProperty);
        }

        public static void SetAction(DependencyObject obj, OperationAction value)
        {
            obj.SetValue(ActionProperty, value);
        }

        //--------Code
        public static string GetCode(DependencyObject obj)
        {
            return (string)obj.GetValue(CodeProperty);
        }

        public static void SetCode(DependencyObject obj, string value)
        {
            obj.SetValue(CodeProperty, value);
        }


        //-------------DependencyProperty
        public static readonly DependencyProperty ActionProperty = DependencyProperty.RegisterAttached("Action", typeof(OperationAction), typeof(Operation), new PropertyMetadata(OperationAction.Disable, (d, e) =>
        {
            var code = Operation.GetCode(d);
            var action = Operation.GetAction(d);
            if (false == string.IsNullOrWhiteSpace(code))
                SetElementPolicy(d, code, action);
        }));

        public static readonly DependencyProperty CodeProperty = DependencyProperty.RegisterAttached("Code", typeof(string), typeof(Operation), new PropertyMetadata(string.Empty, (d, e) =>
        {
            var code = Operation.GetCode(d);
            var action = Operation.GetAction(d);

            if (false == string.IsNullOrWhiteSpace(code))
                SetElementPolicy(d, code, action);
        }));

        private static void SetElementPolicy(DependencyObject element, string code, OperationAction action)
        {
            if (SystemInfo.OperationCodes == null || SystemInfo.OperationCodes.Count == 0)
                return;

            var containCode = SystemInfo.OperationCodes.Contains(code);
            if (containCode)
                return;

            switch (action)
            {
                case OperationAction.Disable:
                    {
                        element.SetValue(Control.IsEnabledProperty, false);
                        element.SetValue(Control.VisibilityProperty, Visibility.Visible);
                        break;
                    }
                case OperationAction.Hide:
                    {
                        element.SetValue(Control.VisibilityProperty, Visibility.Collapsed);
                        element.SetValue(Control.IsEnabledProperty, true);
                        break;
                    }
                default:
                    break;
            }

        }

        private static void ReSet(DependencyObject element)
        {
            if (element == null)
                throw new Exception();

            var code = Operation.GetCode(element);
            if (string.IsNullOrWhiteSpace(code) == false)
            {

                element.SetValue(Operation.CodeProperty, string.Empty);
                element.SetValue(Operation.CodeProperty, code);
            }
        }

        public static void ResetOperationCodes(DependencyObject element)
        {
            if (element == null)
                throw new ArgumentException("element不能為空!", "element");

            if (element is ComboBox)
            {
                var cb = (element as ComboBox);
                cb.DropDownOpened -= OnDropDownOpened;
                cb.DropDownOpened += OnDropDownOpened;
            }
            for (int i = 0, count = VisualTreeHelper.GetChildrenCount(element); i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);
                ReSet(child);
                ResetOperationCodes(child);
            }
        }

        private static void OnDropDownOpened(object sender, System.EventArgs e)
        {
            var items = (sender as ComboBox).Items;
            foreach (var item in items)
            {
                if (item is DependencyObject)
                    ReSet(item as DependencyObject);
            }
        }

    }
}
