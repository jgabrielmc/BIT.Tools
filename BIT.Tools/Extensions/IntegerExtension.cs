using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT.Tools.Extensions
{
    public static class IntegerExtension
    {
        public static Int32 ToInteger(this object value)
        {
            return ToInteger(value, 0);
        }
        public static Int32 ToInteger(this object value, Int32 def)
        {
            Int32 result = 0;
            if (Int32.TryParse(value.ToString(), out result))
                return result;
            return def;
        }

        public static bool IsPrime(this int number)
        {
            if ((number % 2) == 0)
            {
                return number == 2;
            }
            int sqrt = (int)Math.Sqrt(number);
            for (int t = 3; t <= sqrt; t = t + 2)
            {
                if (number % t == 0)
                {
                    return false;
                }
            }
            return number != 1;
        }

        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }

        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }
    }
}
