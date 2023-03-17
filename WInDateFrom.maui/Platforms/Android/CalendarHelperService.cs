using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDateFrom.maui.Platforms.Android
{
      public class CalendarHelperService
      {
            public static bool Set(long cal, String nome, DateTime d)
            {
                return MainActivity.Instance.CalendarHelper(cal, nome, d);
            }
            public static long CreateCalendar()
            {
                return MainActivity.Instance.createLocalCalendar();
            }
            public static void ResetCalendar(long cal)
            {
                MainActivity.Instance.ResetCalendar(cal);
            }
    }
}
