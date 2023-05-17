using Codeblock.ViewModel.BlockViews;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace Codeblock.ViewModel.UnitsView
{
    public class RunPointView
    {
        private StackLayout StartUnitView;
        public BlockView BV;
        public RunPointView(BlockView blockView)
        {
            BV = blockView;
            Label label = new Label()
            {
                Text = "Main",
                TextColor = Color.Black,
                FontSize = 17,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            Frame frame = new Frame() 
            {
                Content = label,
                BackgroundColor = new Color(255 / 255.0, 255 / 255.0, 0 / 255.0),
                WidthRequest = Helpers.Helper.BlockWidth - 2 * Helpers.Helper.Padding,
                CornerRadius = 10,
                HeightRequest = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            StartUnitView = new StackLayout();
            StartUnitView.Children.Add(frame);
        }
        public StackLayout GetRunPointFrame()
        {
            return StartUnitView;
        }
    }
}