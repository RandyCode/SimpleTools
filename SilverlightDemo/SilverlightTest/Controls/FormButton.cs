using System.Windows;
using System.Windows.Controls;

namespace SilverlightTest.Controls
{
    [TemplatePart(Name = CommitName, Type = typeof(Button))]
    [TemplatePart(Name = CancleName, Type = typeof(Button))]
    public class FormButton :ContentControl
    {
        private const string CommitName = "Commit";
        private const string CancleName = "Cancle";
    
        public event RoutedEventHandler Commited;
        public event RoutedEventHandler Canceled;

        private Button _OkButton;
        private Button _CancelButton;
      
        public FormButton()
        {
            this.DefaultStyleKey = typeof(FormButton);
          
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _CancelButton = (Button)GetTemplateChild(CancleName);
            _OkButton = (Button)GetTemplateChild(CommitName);

            if (_CancelButton != null)
            {
                _CancelButton.Click += Canceled;
            }

            if (_OkButton != null)
            {
                _OkButton.Click += Commited;
            }
          
        }
       
    }
}
