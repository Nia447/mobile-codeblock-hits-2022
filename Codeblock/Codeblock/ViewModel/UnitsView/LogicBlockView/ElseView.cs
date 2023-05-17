using Codeblock.Model;
using Codeblock.ViewModel.UnitsView.VariableBlockView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Codeblock.ViewModel.UnitsView.VariableBlockView
{
    public class ElseView
    {
        private StackLayout PositionalStackLayout;
        private StackLayout FrameLayout;

        public LogicObject LogicObject;
        public LogicBlock LogicBlock;
        public ElseView(LogicBlock logicBlock)
        {
            LogicBlock = logicBlock;
            LogicObject = new LogicObject(LogicBlock.CurrentCodeBlock);
            PositionalStackLayout = new StackLayout();
            ComposeElseView();
        }
        public void ComposeElseView()
        {
            FrameLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = new Color(95 / 255.0, 158 / 255.0, 160 / 255.0),
            };

            Entry elseValueEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Placeholder = "Expression",
            };
            elseValueEntry.TextChanged += EntryValueChanged;

            FrameLayout.Children.Add(elseValueEntry);

            Frame frame = new Frame()
            {
                Content = FrameLayout,
                CornerRadius = 10,
                Padding = 0,
                BorderColor = new Color(95 / 255.0, 158 / 255.0, 160 / 255.0),
            };
            PositionalStackLayout.Children.Add(frame);
        }
        public void EntryValueChanged(object sender, TextChangedEventArgs e)
        {
            LogicObject.Input = e.NewTextValue;
        }
        public StackLayout GetElseView()
        {
            return PositionalStackLayout;
        }
    }
}
