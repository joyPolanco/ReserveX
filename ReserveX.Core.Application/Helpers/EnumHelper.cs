using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Helpers
{
    public static class EnumHelper
    {

        public static T GetEnumValueFromString<T> (string value) where T : struct,Enum
        {
            if (Enum.TryParse<T>(value,true, out var enumValue))

                    return enumValue;
            throw new ArgumentException(
       $"The value '{value}' is not valid for {typeof(T).Name}");
        }

    }
}
