
using System.Collections.Generic;
using System.Globalization;

namespace MyBus.Constants
{
    public static class ConstantData
    {
        public static IEnumerable<CultureInfo> Languages
        {
            get
            {
                yield return new CultureInfo("en-US");
                yield return new CultureInfo("ru-RU");
            }
        }
    }
}
