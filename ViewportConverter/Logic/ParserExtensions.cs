using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ViewportConverter.Logic
{
    public static class ParserExtensions
    {
        public static double? ParseAsDouble(this string @string)
        {
            return @string
                .ValidateForNull()
                .PurgeStringValue()
                .TryParse();
        }

        private static readonly Regex _pxRegex = new Regex(@"(px|\s)?");
        private static string PurgeStringValue(this string @string)
        {
            @string = @string
                .Trim()
                .Replace(",", ".");
            return _pxRegex.Replace(@string, "");
        }

        private const string ValidateErrorMessage = "Нельзя передавать пустые значения";
        private static string ValidateForNull(this string @string)
        {
            if (@string == null)
                throw new ArgumentNullException(message: ValidateErrorMessage, paramName: nameof(@string));
            return @string;
        }

        private static double? TryParse(this string @string)
        {
            return double.TryParse(@string, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out double value)
                ? value
                : (double?)null;
        }
    }
}