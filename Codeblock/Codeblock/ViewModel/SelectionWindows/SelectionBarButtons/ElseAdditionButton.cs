using Codeblock.ViewModel.UnitsView.VariableBlockView;
using System;
using Xamarin.Forms;
using Codeblock.Model;
namespace Codeblock.ViewModel.SelectionWindows.SelectionBarButtons
{
    public class ElseAdditionButton
    {
        private Button elseAdditionButton;
        private StackLayout LayoutToAddUnit;
        private AbsoluteLayout AbsoluteParent;
        private Button AdditionButton;

        private LogicBlock LogicBlock;
        private CodeBlock CodeBlock;
        public ElseAdditionButton(StackLayout layoutYoAddUnit, AbsoluteLayout absoluteParent, Button additionButton, CodeBlock codeBlock, LogicBlock logicBlock = null)
        {
            LayoutToAddUnit = layoutYoAddUnit;
            AbsoluteParent = absoluteParent;
            AdditionButton = additionButton;
            LogicBlock = logicBlock;
            CodeBlock = codeBlock;
            elseAdditionButton = new Button()
            {
                Text = "Else Block",
                BackgroundColor = new Color(95 / 255.0, 158 / 255.0, 160 / 255.0),
                CornerRadius = 10,
            };
            elseAdditionButton.Clicked += AddElse;
        }
        private void AddElse(object sender, EventArgs e)
        {
            LayoutToAddUnit.Children.Add(AdditionButton);
            LayoutToAddUnit.Children.Remove(AdditionButton);
            LayoutToAddUnit.Children.Add(new ElseView(LogicBlock).GetElseView());
            AbsoluteParent.Children.Remove((View)elseAdditionButton.Parent.Parent);
        }
        public Button GetElseAdditionButton()
        {
            return elseAdditionButton;
        }
    }
}
