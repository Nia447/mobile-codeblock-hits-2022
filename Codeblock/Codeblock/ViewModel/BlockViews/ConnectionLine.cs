using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace Codeblock.ViewModel.BlockViews
{
    public class ConnectionLine
    {
        private Line Line;
        public Color Color;
        public ConnectionLine(Color color)
        {
            Color = color;
            Line = new Line()
            {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = 1,
                StrokeLineCap = PenLineCap.Round
            };
        }
        public Line DrowLine(Point StartPoint, Point EndPoint)
        {
            Line.X1 = StartPoint.X;
            Line.Y1 = StartPoint.Y;
            Line.X2 = EndPoint.X;
            Line.Y2 = EndPoint.Y;
            return Line;
        }
        public Line GetLine()
        {
            return Line;
        }
    }
}
