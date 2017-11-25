using System;

namespace ViewportConverter.Logic
{
    public class Converter
    {
        public double? StandardWidth { get; private set; }
        public double? StandardHeight { get; private set; }

        public bool HasStandardValues() =>
            StandardWidth.HasValue && StandardHeight.HasValue;

        public void SetStandardValues(string pxWidth, string pxHeight)
        {
            StandardWidth = ParseAsDouble(pxWidth) / 100;
            StandardHeight = ParseAsDouble(pxHeight) / 100;
        }

        public double ParseAsDouble(string @string)
            => @string.ParseAsDouble()
               ?? throw new ArgumentException(message: @"Возникла ошибка при обработке числа в тип double", paramName: nameof(@string));

        public string GetAsViewportWidth(string @string)
        {
            return string.IsNullOrWhiteSpace(@string)
                ? null
                : $"{CalculateViewportWidth(ParseAsDouble(@string))}vw";
        }

        public string GetAsViewportHeight(string @string)
        {
            return string.IsNullOrWhiteSpace(@string) 
                ? null 
                : $"{CalculateViewportHeight(ParseAsDouble(@string))}vh";
        }

        private double CalculateViewportWidth(double value)
        {
            if (StandardWidth == null)
                throw new NullReferenceException("Не установлена эталонная ширина");
            return value / StandardWidth.Value;
        }

        private double CalculateViewportHeight(double value)
        {
            if (StandardHeight == null)
                throw new NullReferenceException("Не установлена эталонная высота");
            return value / StandardHeight.Value;
        }
    }
}