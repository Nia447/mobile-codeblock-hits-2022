using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Codeblock.ViewModel.UnitsView.OutputBlockFolder
{
    public class OutputPartView
    {
        private StackLayout StackLayout;
        private Label OutputLabel;
        public OutputPartView()
        {
            StackLayout = new StackLayout();
            OutputLabel = new Label()
            {
                FontSize = Helpers.Helper.FontSize,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Color.Black,
            };
            ComposeOutpartView();
        }
        private void ComposeOutpartView()
        {
            StackLayout frameLayout = new StackLayout();
            frameLayout.Children.Add(OutputLabel);

            Frame frame = new Frame()
            {
                Content = frameLayout,
                CornerRadius = Helpers.Helper.Radius,
                BackgroundColor = Color.White,
                BorderColor = Color.DeepPink,
            };
            StackLayout.Children.Add(frame);
        }
        public void WriteLine(string text)
        {
            OutputLabel.Text += text + "\n";
        }
        public View GetOutputPartView()
        {
            return StackLayout;
        }
    }
}
