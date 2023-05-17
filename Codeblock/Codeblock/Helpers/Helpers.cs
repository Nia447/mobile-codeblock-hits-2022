using Xamarin.Forms;

namespace Codeblock.Helpers
{
    public static class Helper
    {
        static public int Padding = 20;
        static public int Spacing = 10;
        static public int BlockSpacing = 70;
        static public int BlockWidth = 260;
        static public Color RunPointColor = Color.Yellow;
        static public int Radius = 10;
        static public int FontSize = 18;
        static public double Bias = System.Math.Sqrt(2 * System.Math.Pow(Radius, 2)) - Radius;
        static public Point GetCurrentPoint(Point point, int n)//n - number in stack
        {
            return new Point(point.X + BlockWidth - Padding - Bias, point.Y + Padding + Bias + (n * Spacing));
        }
    }
}
