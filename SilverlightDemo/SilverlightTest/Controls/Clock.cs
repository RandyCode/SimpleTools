using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SilverlightTest.Controls
{

    [TemplatePart(Name = "ellipseBg", Type = typeof(Shape))]
    [TemplatePart(Name = "lineHor", Type = typeof(Shape))]
    [TemplatePart(Name = "lineMin", Type = typeof(Shape))]
    [TemplatePart(Name = "lineSecond", Type = typeof(Shape))]
    public class Clock : Control
    {

        public Clock()
        {
            DefaultStyleKey = typeof(Clock);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _lnHor = (Line)GetTemplateChild("lineHor");
            _lnMin = (Line)GetTemplateChild("lineMin");
            _lnSec = (Line)GetTemplateChild("lineSecond");

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(1);
            _timer.Start();
            _timer.Tick += new EventHandler(timer_Tick);

        }

        private DispatcherTimer _timer;
        private const int _secLen = 45;
        private const int _minLen = 38;
        private const int _horLen = 30;
        private Line _lnHor;
        private Line _lnMin;
        private Line _lnSec;
        private int _sec;
        private int _min;
        private int _hor;
        private double _angle;
        private DateTime _dtNow;



        public string DatetimeNow
        {
            get { return (string)GetValue(DatetimeNowProperty); }
            set { SetValue(DatetimeNowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DatetimeNow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DatetimeNowProperty =
            DependencyProperty.Register("DatetimeNow", typeof(string), typeof(Clock
            ), new PropertyMetadata(""));



        private void timer_Tick(object sender, EventArgs e)
        {
            _dtNow = DateTime.Now;
            DatetimeNow = _dtNow.ToLongTimeString();
            _sec = _dtNow.Second;
            _min = _dtNow.Minute;
            _hor = _dtNow.Hour;

            _angle = Math.PI / 30 * _sec - Math.PI / 2;

            if (_lnHor != null && _lnMin != null && _lnHor != null)
            {
                _lnSec.X2 = 50 + Math.Cos(_angle) * _secLen;
                _lnSec.Y2 = 50 + Math.Sin(_angle) * _secLen;

                _angle = Math.PI / 1800 * (_min * 60 + _sec) - Math.PI / 2;

                _lnMin.X2 = 50 + Math.Cos(_angle) * _minLen;
                _lnMin.Y2 = 50 + Math.Sin(_angle) * _minLen;

                _angle = Math.PI / 21600 * (_hor * 3600 + _min * 60 + _sec) - Math.PI / 2;

                _lnHor.X2 = 50 + Math.Cos(_angle) * _horLen;
                _lnHor.Y2 = 50 + Math.Sin(_angle) * _horLen;
            }
        }



    }
}
