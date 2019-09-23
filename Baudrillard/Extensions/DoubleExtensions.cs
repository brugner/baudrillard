using System;

namespace Baudrillard.Extensions
{
    public static class DoubleExtensions
    {
        public static double? Round(this double? number, int decimals)
        {
            if (number.HasValue)
            {
                return Math.Round(number.Value, decimals);
            }
            else
            {
                return null;
            }
        }
    }
}
