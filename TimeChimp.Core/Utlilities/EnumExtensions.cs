using System;
using System.Collections.Generic;
using System.Text;

namespace TimeChimp.Core.Utlilities
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value) where T : struct
        {
            if (Enum.TryParse(value, out T result))
            {
                return result;
            }
            else
            {
                return default(T);
            }
        }
    }
}
