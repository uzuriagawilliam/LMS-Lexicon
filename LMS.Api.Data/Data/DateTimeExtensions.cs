using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Api.Data.Data
{
    public static class DateTimeExtensions
    {        
        public static int GetCurrentAge(this DateTime dateTime)
        {
            var currentDate = DateTime.Now;
            int age = currentDate.Year - dateTime.Year;
            if (currentDate < dateTime.AddYears(age))
                age--;
            return age;
        }
    }
}
