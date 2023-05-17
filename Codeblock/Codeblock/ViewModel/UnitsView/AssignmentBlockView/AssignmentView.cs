using Codeblock.Model;
using Codeblock.ViewModel.UnitsView.VariableBlockView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Codeblock.ViewModel.UnitsView.VariableBlockView
{
    public class AssignmentView
    {
        private StackLayout PositionalStackLayout;
        private StackLayout FrameLayout;

        public Variable Variable;
        public CodeBlock CodeBlock;
        public AssignmentView(CodeBlock codeBlock)
        {
            CodeBlock = codeBlock;
            Variable = new Variable();
            Variable.Assignment = true;
            CodeBlock.AddVariable(Variable);
            PositionalStackLayout = new StackLayout();
            ComposeAssignmentView();
        }
        public void ComposeAssignmentView()
        {
            FrameLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = new Color(250 / 255.0, 128 / 255.0, 114 / 255.0),
            };

            Entry variableNameEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Placeholder = "Name",
            };
            Entry variableValueEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Placeholder = "Value",
            };
            variableNameEntry.TextChanged += EntryNameChanged;
            variableValueEntry.TextChanged += EntryValueChanged;

            FrameLayout.Children.Add(variableNameEntry);
            FrameLayout.Children.Add(variableValueEntry);

            Frame frame = new Frame()
            {
                Content = FrameLayout,
                CornerRadius = 10,
                Padding = 0,
                BorderColor = new Color(181 / 255.0, 0 / 255.0, 0 / 255.0),
            };
            PositionalStackLayout.Children.Add(frame);
        }
        public void EntryNameChanged(object sender, TextChangedEventArgs e)
        {
            Variable.Name = e.NewTextValue;
        }
        public void EntryValueChanged(object sender, TextChangedEventArgs e)
        {
            Variable.Input = e.NewTextValue;
        }
        public StackLayout GetAssignmentView()
        {
            return PositionalStackLayout;
        }
    }
}
