using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Enums
{
    public class Enums
    {
        public enum OperatorComparer
        {
            Contains = 0,
            StartsWith = 1,
            EndsWith = 2,
            Equals = 13,
            GreaterThan = 15,
            GreaterThanOrEqual = 16,
            LessThan = 20,
            LessThanOrEqual = 20,
            NotEqual = 35
        }
        public enum WeekDays
        {
            Sun = 0,
            Mon = 1,
            Tue = 2,
            Wed = 3,
            Thu = 4,
            Fri = 5,
            Sat = 6
        }
        public enum OrderDirection
        {
            Descending = 1,
            Ascending = 2
        }
    }
}
