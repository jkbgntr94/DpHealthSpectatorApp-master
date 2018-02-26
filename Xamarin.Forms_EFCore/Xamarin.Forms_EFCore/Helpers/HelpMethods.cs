using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms_EFCore.Helpers
{
    public class HelpMethods
    {
        public string GetActualTime()
        {
            String timeStamp = GetTimestamp(DateTime.Now);
            return timeStamp;
        }

        private static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}
