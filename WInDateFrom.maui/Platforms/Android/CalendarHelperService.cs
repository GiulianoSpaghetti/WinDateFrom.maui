using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDateFrom.maui.Platforms.Android
{
      public class CalendarHelperService
      {
            public static bool Set(String nome, DateTime d)
            {
                return MainActivity.Instance.CalendarHelper(nome, d);
            }
      }
}
