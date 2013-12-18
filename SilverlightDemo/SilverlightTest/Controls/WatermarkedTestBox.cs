using System;
using System.Windows;
using System.Windows.Controls;

namespace SilverlightTest.Controls
{
    [TemplatePart(Name = "Watermark", Type = typeof(ContentControl))]
    [TemplateVisualState(Name = "Unwatermarked", GroupName = "GroupWatermark")]
    [TemplateVisualState(Name = "Watermarked", GroupName = "GroupWatermark")]
    [TemplateVisualState(Name = "FastUnmarked", GroupName = "GroupWatermark")]
    public class WatermarkedTestBox : TextBox
    {

        /// <summary>
        /// Watermark dependency property
        /// </summary>
        public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.Register(
            "Watermark", typeof(object), typeof(WatermarkedTestBox), null);

        /// <summary>
        /// Watermark content
        /// </summary>
        /// <value>The watermark.</value>
        public object WatermarkText
        {
            get { return (object)GetValue(WatermarkTextProperty); }
            set { SetValue(WatermarkTextProperty, value); }
        }



        public WatermarkedTestBox()
        {
            DefaultStyleKey = typeof(WatermarkedTestBox);
            WatermarkText = "press to disappeare";
           
        }

        private void OnWatermarkedTestBoxLoaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Text))
                GoToState(this,true, "FastUnmarked");
        }


        private void OnWatermarkedTestBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                GoToState(this, true, "Watermarked");
            }
   

        }

        private void OnWatermarkedTestBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Text))
                GoToState(this, true, "FastUnmarked");
            else
                GoToState(this,  true,"UnWatermarked");
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.GotFocus += OnWatermarkedTestBoxGotFocus;
            this.LostFocus += OnWatermarkedTestBoxLostFocus;
            this.Loaded += OnWatermarkedTestBoxLoaded;
        }


        private void GoToState(Control control, bool userTransitions, params string[] stateNames)
        {
            if (control == null)
                throw new ArgumentException("control");
            if (stateNames != null)
            {
                foreach (string str in stateNames)
                {
                    if (VisualStateManager.GoToState(control, str, userTransitions))
                        return;
                }
            }
        }

    }
}
