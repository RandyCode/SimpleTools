using System.Globalization;
using System.Threading;

namespace SilverlightTest.Helper
{
    public static class DateTimeFormatUtil
    {
        public static void ChangeCurrentDateTimeFormat()
        {
            CultureInfo culinfo = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
            DateTimeFormatInfo dinfo = (DateTimeFormatInfo)Thread.CurrentThread.CurrentCulture.DateTimeFormat.Clone(); //这个是只读的,所以先复制一份
            dinfo.ShortDatePattern = "yyyy-MM-dd";
            dinfo.LongTimePattern = "HH:mm:ss UTCzzz";
            culinfo.DateTimeFormat = dinfo;
            Thread.CurrentThread.CurrentCulture = culinfo;
            Thread.CurrentThread.CurrentUICulture = culinfo;
        }
    }
}
