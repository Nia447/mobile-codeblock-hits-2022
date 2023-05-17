using Codeblock.ViewModel.UnitsView.VariableBlockView;
using System;
using Xamarin.Forms;
using Codeblock.Model;
using Codeblock.ViewModel.UnitsView.LogicBlockView;
namespace Codeblock.ViewModel.SelectionWindows.SelectionBarButtons
{
    public class LogicBlockAdditionButton
    {
        private Button logicBlockAdditionButton;
        private StackLayout LayoutToAddUnit;
        private AbsoluteLayout AbsoluteParent;
        private Field Field;
        private Button AdditionButton;

        private CodeBlock CodeBlock;
        public LogicBlockAdditionButton(StackLayout layoutYoAddUnit, AbsoluteLayout absoluteParent, Field field, Button additionBUtton, CodeBlock codeBlock)
        {
            LayoutToAddUnit = layoutYoAddUnit;
            AbsoluteParent = absoluteParent;
            AdditionButton = additionBUtton;
            Field = field;
            CodeBlock = codeBlock;
            logicBlockAdditionButton = new Button()
            {
                Text = "Logic Block",
                BackgroundColor = new Color(0 / 255.0, 255 / 255.0, 255 / 255.0),
                CornerRadius = 10,
            };
            logicBlockAdditionButton.Clicked += AddVariable;
        }
        private void AddVariable(object sender, EventArgs e)
        {
            LayoutToAddUnit.Children.Remove(AdditionButton);
            LayoutToAddUnit.Children.Add(new LogicView(AbsoluteParent, Field, CodeBlock).GetLogicView());
            LayoutToAddUnit.Children.Add(AdditionButton);
            AbsoluteParent.Children.Remove((View)logicBlockAdditionButton.Parent.Parent);
        }
        public Button GetLogicBlockAdditionButton()
        {
            return logicBlockAdditionButton;
        }
    }
}
