using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Codeblock.Model;

namespace Codeblock.ViewModel.UnitsView.OutputBlockFolder
{
    public class InputPartView
    {
        private StackLayout StackLayout;
        private Entry Entry;
        private OutputBlock OutputBlock;
        public InputPartView(OutputBlock outputBlock)
        {
            OutputBlock = outputBlock;
            StackLayout = new StackLayout();
            Entry = new Entry()
            {
                TextColor = Color.Black,
                FontSize = Helpers.Helper.FontSize,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "Names",
            };
            ComposeInputView();
        }
        private void ComposeInputView()
        {
            StackLayout frameLayout = new StackLayout();
            Entry.TextChanged += EntryInputChanged;
            frameLayout.Children.Add(Entry);

            Frame frame = new Frame()
            {
                Content = frameLayout,
                BackgroundColor = Color.White,
                Padding = Helpers.Helper.Padding - 10,
                CornerRadius = Helpers.Helper.Radius,
                BorderColor = Color.DeepPink,
            };
            StackLayout.Children.Add(frame);
        }
        private void EntryInputChanged(object sender, TextChangedEventArgs e)
        {
            OutputBlock.Input = e.NewTextValue;
        }
        public View GetInputPartView()
        {
            return StackLayout;
        }
    }
}
