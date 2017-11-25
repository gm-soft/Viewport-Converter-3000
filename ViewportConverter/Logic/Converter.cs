using System;

namespace ViewportConverter.Logic
{
    public class Converter
    {
        private const string WidthParseErrorMessage = "Не удалось распарсить ширину";
        private const string HeightParseErrorMessage = "Не удалось распрасить высоту";

        public ViewportData ConvertViewportData(string pxWidth, string pxHeight)
        {
            double width = pxWidth.ParseAsDouble() 
                ?? throw new ArgumentException(message: WidthParseErrorMessage, paramName: nameof(pxWidth));

            double height = pxHeight.ParseAsDouble()
                           ?? throw new ArgumentException(message: HeightParseErrorMessage, paramName: nameof(pxHeight));

            return new ViewportData(width, height);
        }
    }
}