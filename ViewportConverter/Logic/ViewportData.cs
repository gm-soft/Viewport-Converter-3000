namespace ViewportConverter.Logic
{
    public class ViewportData : WidthHeightData
    {
        public ViewportData(double width, double height) 
            : base(width, height) { }

        public string GetWidthAsString() => $"{Width}vw";
        public string GetHeightAsString() => $"{Height}vh";

        public static ViewportData Create(WidthHeightData data)
        {
            return new ViewportData(data.Width, data.Height);
        }
    }
}