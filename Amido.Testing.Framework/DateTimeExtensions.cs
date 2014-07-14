using System;
using System.Globalization;

namespace Amido.Testing.Framework
{
    public static class DateTimeExtensions
    {
        private const string DateTimeFormat = "{0}-{1}-{2}T{3}:{4}:{5}Z";

        public enum DatePart
        {
            Year = 0,
            Month = 1,
            Day = 2
        }

        public static string ToIso8601Date(this DateTime date)
        {
            return string.Format(
                DateTimeFormat,
                date.Year,
                PadLeft(date.Month),
                PadLeft(date.Day),
                PadLeft(date.Hour),
                PadLeft(date.Minute),
                PadLeft(date.Second));
        }

        public static DateTime FromIso8601Date(this string date)
        {
            return DateTime.ParseExact(date.Replace("T", " "), "u", CultureInfo.InvariantCulture);
        }

        public static string ToYearMonthDayHourString(this DateTime datetime)
        {
            return datetime.ToString("yyyyMMddHH");
        }

        public static string ToMinuteSecondMillisecond(this DateTime datetime)
        {
            return datetime.ToString("mmssfff");
        }

        public static string ToYearMonthDayHourMinuteString(this DateTime datetime)
        {
            return datetime.ToString("yyyyMMddHHmm");
        }

        public static string ToYearMonthDayHourMinuteSecondMilliSecondString(this DateTime datetime)
        {
            return datetime.ToString("yyyyMMddHHmmssffff");
        }

        public static int GetValueFromIso8601DateString(string date, DatePart datePart)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return 0;
            }

            var parts = date.Split('-');

            if (parts.Length != 3)
            {
                return 0;
            }

            var positionParts = parts[(int)datePart];

            int result;

            return !int.TryParse(positionParts, out result) ? 0 : result;
        }

        private static string PadLeft(int number)
        {
            if (number < 10)
            {
                return string.Format("0{0}", number);
            }

            return number.ToString(CultureInfo.InvariantCulture);
        }
    }
}