using Codeblock.Model;
using Codeblock.ViewModel.UnitsView.VariableBlockView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Codeblock.ViewModel.UnitsView.VariableBlockView
{
    public class ElifView
    {
        private StackLayout PositionalStackLayout;
        private StackLayout FrameLayout;

        public LogicObject LogicObject;
        public LogicBlock LogicBlock;
        public ElifView(LogicBlock logicBlock)
        {
            LogicBlock = logicBlock;
            LogicObject = new LogicObject(LogicBlock.CurrentCodeBlock);
            LogicBlock.AddElseIf(LogicObject);
            PositionalStackLayout = new StackLayout();
            ComposeElifView();
        }
        public void ComposeElifView()
        {
            FrameLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = new Color(70 / 255.0, 130 / 255.0, 180 / 255.0),
            };

            Entry elifValueEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Placeholder = "Expression",
            };
            elifValueEntry.TextChanged += EntryValueChanged;

            FrameLayout.Children.Add(elifValueEntry);

            Frame frame = new Frame()
            {
                Content = FrameLayout,
                CornerRadius = 10,
                Padding = 0,
                BorderColor = new Color(70 / 255.0, 130 / 255.0, 180 / 255.0),
            };
            PositionalStackLayout.Children.Add(frame);
        }
        public void EntryValueChanged(object sender, TextChangedEventArgs e)
        {
            LogicObject.Input = e.NewTextValue;
        }
        public StackLayout GetElifView()
        {
            return PositionalStackLayout;
        }
    }
}
