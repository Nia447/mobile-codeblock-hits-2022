using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Codeblock.Model;

namespace Codeblock.ViewModel.UnitsView.OutputBlockFolder
{
    public class OutputBlockView
    {
        OutputBlock OutputBlock;
        InputPartView InputPart;
        OutputPartView OutputPart;
        StackLayout StackLayout;
        public CodeBlock CodeBlock;
        public OutputBlockView(CodeBlock codeBlock)
        {
            OutputPart = new OutputPartView();
            OutputBlock = new OutputBlock(OutputPart);
            InputPart = new InputPartView(OutputBlock);
            StackLayout = new StackLayout();
            CodeBlock = codeBlock;

            ComposeOutputBlock();
        }
        private void ComposeOutputBlock()
        {
            CodeBlock.AddOutputBlock(OutputBlock);
            StackLayout FrameLayout = new StackLayout()
            {
                Padding = 3,
                Spacing = 3,
                BackgroundColor = Color.DeepPink,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            FrameLayout.Children.Add(InputPart.GetInputPartView());
            FrameLayout.Children.Add(OutputPart.GetOutputPartView());
            Frame frame = new Frame()
            {
                Content = FrameLayout,
                CornerRadius = Helpers.Helper.Radius,
                BorderColor = Color.DeepPink,
                Padding = 0,
            };
            StackLayout.Children.Add(frame);
        }
        public View GetOutputBlock()
        {
            return StackLayout;
        }
        //TODO: Remove AddOutputPartToAbsoluteLayout() and GetInputPart()
        public View AddOutputPartToAbsoluteLayout()
        {
            return OutputPart.GetOutputPartView();
        }
        public View GetInputPart()
        {
            return InputPart.GetInputPartView();
        }
    }
}