using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSupport
{
    public static class ExtensionMethods
    {
        public static int ToInt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value");
            }

            int result = -1;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            
            throw new ArgumentException();
        }
    }
}
