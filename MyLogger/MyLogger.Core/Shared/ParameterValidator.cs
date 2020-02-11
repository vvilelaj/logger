using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Shared
{
    public class ParameterValidator
    {
        public static void ThowExceptionWhenIsNullOrEmpty(string paramValue, string paramName, string methodName)
        {
            if (string.IsNullOrEmpty(paramValue)) throw new ArgumentNullException(paramName, $"{methodName} : {paramName} is null or empty.");
        }

        public static void ThowExceptionWhenIsNull(object paramValue, string paramName, string methodName)
        {
            if (paramValue == null) throw new ArgumentNullException(paramName, $"{methodName} : {paramName} is null.");
        }
    }
}
