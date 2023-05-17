using Codeblock.ViewModel.UnitsView;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Codeblock.Model;
using Xamarin.Forms.Shapes;
using Codeblock.ViewModel.BlockViews;
using Codeblock.ViewModel.UnitsView.VariableBlockView;

namespace Codeblock.ViewModel.UnitsView.LogicBlockView
{
    public class LogicView
    {
        private StackLayout BlockLayout;
        private AbsoluteLayout AbsoluteParent;
        private Field Field;

        public LogicBlock LogicBlock;
        public CodeBlock CodeBlock;
        public LogicView(AbsoluteLayout absoluteParent, Field field, CodeBlock codeBlock)
        {
            AbsoluteParent = absoluteParent;
            Field = field;
            CodeBlock = codeBlock;
            LogicBlock = new LogicBlock(codeBlock);
            codeBlock.AddLogicBlock(LogicBlock);
            BlockLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
            };
            ComposeLogicView();
        }
        private void ComposeLogicView()
        {/*
            SetAllLines();*/

            StackLayout FrameLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Vertical,
            };

            NewUnitButton NUB = new NewUnitButton(false, FrameLayout, AbsoluteParent, Field, CodeBlock, true, LogicBlock);

            IfView ifView = new IfView(LogicBlock, AbsoluteParent, Field);

            FrameLayout.Children.Add(ifView.GetIfView());

            FrameLayout.Children.Add(NUB.GetUnitBtn());

            Frame frame = new Frame()
            {
                Content = FrameLayout,
                BackgroundColor = new Color(52.0 / 255.0, 0.0 / 255.0, 156.0 / 255.0),
                CornerRadius = 10,
                Margin = 0,
                BorderColor = Color.Aqua,
                WidthRequest = Helpers.Helper.BlockWidth,
                Padding = Helpers.Helper.Padding,
            };

            BlockLayout.Children.Add(frame);
        }

        public View GetLogicView()
        {
            return BlockLayout;
        }
    }
}