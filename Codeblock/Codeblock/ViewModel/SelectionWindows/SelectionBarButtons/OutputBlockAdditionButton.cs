using Codeblock.ViewModel.UnitsView.OutputBlockFolder;
using System;
using Xamarin.Forms;
using Codeblock.Model;

namespace Codeblock.ViewModel.SelectionWindows.SelectionBarButtons
{
    public class OutputBlockAdditionButton
    {
        private Button outputBlockAdditionButton;
        private AbsoluteLayout AbsoluteParent;
        private StackLayout LayoutToAddUnit;
        private Button AdditionButton;

        public CodeBlock CodeBlock;
        public OutputBlockAdditionButton(StackLayout layoutToAddunit, AbsoluteLayout absoluteParent, Button additionButton, CodeBlock codeBlock)
        {
            AbsoluteParent = absoluteParent;
            LayoutToAddUnit = layoutToAddunit;
            AdditionButton = additionButton;
            CodeBlock = codeBlock;
            outputBlockAdditionButton = new Button()
            {
                Text = "Output",
                BackgroundColor = Color.DeepPink,
                CornerRadius = Helpers.Helper.Radius,
            };
            outputBlockAdditionButton.Clicked += AddOutputBlock;
        }
        private void AddOutputBlock(object sender, EventArgs e)
        {
            LayoutToAddUnit.Children.Remove(AdditionButton);
            LayoutToAddUnit.Children.Add(new OutputBlockView(CodeBlock).GetOutputBlock());
            LayoutToAddUnit.Children.Add(AdditionButton);
            AbsoluteParent.Children.Remove((View)outputBlockAdditionButton.Parent.Parent);
        }
        public Button GetOutputBlockAdditionButton()
        {
            return outputBlockAdditionButton;
        }
    }
}
