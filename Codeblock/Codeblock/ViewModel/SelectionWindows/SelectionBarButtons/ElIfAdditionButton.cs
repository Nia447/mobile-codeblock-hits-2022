using Codeblock.ViewModel.UnitsView.VariableBlockView;
using System;
using Xamarin.Forms;
using Codeblock.Model;
namespace Codeblock.ViewModel.SelectionWindows.SelectionBarButtons
{
    public class ElifAdditionButton
    {
        private Button elifAdditionButton;
        private StackLayout LayoutToAddUnit;
        private AbsoluteLayout AbsoluteParent;
        private Button AdditionButton;

        private LogicBlock LogicBlock;
        private CodeBlock CodeBlock;
        public ElifAdditionButton(StackLayout layoutYoAddUnit, AbsoluteLayout absoluteParent, Button additionBUtton, CodeBlock codeBlock, LogicBlock logicBlock = null)
        {
            LayoutToAddUnit = layoutYoAddUnit;
            AbsoluteParent = absoluteParent;
            AdditionButton = additionBUtton;
            LogicBlock = logicBlock;
            CodeBlock = codeBlock;
            elifAdditionButton = new Button()
            {
                Text = "Elif Block",
                BackgroundColor = new Color(70 / 255.0, 130 / 255.0, 180 / 255.0),
                CornerRadius = 10,
            };
            elifAdditionButton.Clicked += AddElif;
        }
        private void AddElif(object sender, EventArgs e)
        {
            LayoutToAddUnit.Children.Remove(AdditionButton);
            LayoutToAddUnit.Children.Add(new ElifView(LogicBlock).GetElifView());
            LayoutToAddUnit.Children.Add(AdditionButton);
            AbsoluteParent.Children.Remove((View)elifAdditionButton.Parent.Parent);
        }
        public Button GetElifAdditionButton()
        {
            return elifAdditionButton;
        }
    }
}
