using System;

namespace Form_Functions
{
    public class TimeSpanFormatProvider : IFormatProvider, ICustomFormatter
    {
        /// <inheritdoc />
        public object GetFormat(Type formatType) => formatType == typeof(ICustomFormatter) ? this : null;

        /// <inheritdoc />
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            TimeSpan timeSpan;
            if (arg is TimeSpan argTimeSpan && format.ToUpper() == "N")
            {
                timeSpan = argTimeSpan;
            }
            else 
                throw new ArgumentException(@"arg must be Time Span", nameof(arg));

            var finalResult = "";
            if (timeSpan.Days > 0) 
                finalResult += $"{timeSpan.Days} Day{(timeSpan.Days == 1 ? "" : "s")}, "; 
            finalResult += $"{AddZerosIfNecessary(timeSpan.Hours)}:";
            finalResult += $"{AddZerosIfNecessary(timeSpan.Minutes)}:";
            finalResult += $"{AddZerosIfNecessary(timeSpan.Seconds)}";
            return finalResult;
        }

        private static string AddZerosIfNecessary(int num) => num is >= 0 and <= 9 ? "0" + num : num.ToString();
    }
}
