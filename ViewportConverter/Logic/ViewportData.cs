namespace ViewportConverter.Logic
{
    public class ViewportData
    {
        public double Width { get; }

        public double Height { get; }

        public ViewportData(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public string GetWidthAsString() => $"{Width}vw";
        public string GetHeightAsString() => $"{Height}vh";
    }
}