using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT.Tools.Extensions
{
    public static class BooleanExtension
    {
        public static Boolean ToBoolean(this object val)
        {
            return ToBoolean(val, false);
        }
        public static Boolean ToBoolean(this object val, Boolean def)
        {
            Boolean reval = false;

            if (Boolean.TryParse(val.ToString(), out reval))
                return reval;

            return def;
        }
    }
}
