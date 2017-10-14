using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT.Tools.Extensions
{
    public static class DecimalExtension
    {
        public static Decimal ToDecimal(this object val)
        {
            return ToDecimal(val, 0);
        }
        public static Decimal ToDecimal(this object val, Decimal def)
        {
            Decimal reval = 0;

            if (Decimal.TryParse(val.ToString(), out reval))
                return reval;
            return def;
        }
    }
}
