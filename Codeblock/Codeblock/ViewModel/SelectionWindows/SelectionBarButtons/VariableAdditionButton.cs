using Codeblock.ViewModel.UnitsView.VariableBlockView;
using System;
using Xamarin.Forms;
using Codeblock.Model;
namespace Codeblock.ViewModel.SelectionWindows.SelectionBarButtons
{
    public class VariableAdditionButton
    {
        private Button variableAdditionButton;
        private StackLayout LayoutToAddUnit;
        private AbsoluteLayout AbsoluteParent;
        private Button AdditionButton;

        private CodeBlock CodeBlock;
        public VariableAdditionButton(StackLayout layoutYoAddUnit, AbsoluteLayout absoluteParent, Button additionBUtton, CodeBlock codeBlock)
        {
            LayoutToAddUnit = layoutYoAddUnit;
            AbsoluteParent = absoluteParent;
            AdditionButton = additionBUtton;
            CodeBlock = codeBlock;
            variableAdditionButton = new Button()
            {
                Text = "Variable",
                BackgroundColor = new Color(181 / 255.0, 0 / 255.0, 0 / 255.0),
                CornerRadius = 10,
            };
            variableAdditionButton.Clicked += AddVariable;
        }
        private void AddVariable(object sender, EventArgs e)
        {
            LayoutToAddUnit.Children.Remove(AdditionButton);
            LayoutToAddUnit.Children.Add(new VariableView(CodeBlock).GetVariableView());
            LayoutToAddUnit.Children.Add(AdditionButton);
            AbsoluteParent.Children.Remove((View)variableAdditionButton.Parent.Parent);
        }
        public Button GetVariableAdditionButton() 
        { 
            return variableAdditionButton;
        }
    }
}
