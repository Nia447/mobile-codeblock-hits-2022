using Codeblock.ViewModel.UnitsView.VariableBlockView;
using System;
using Xamarin.Forms;
using Codeblock.Model;
namespace Codeblock.ViewModel.SelectionWindows.SelectionBarButtons
{
    public class AssignmentAdditionButton
    {
        private Button assignmentAdditionButton;
        private StackLayout LayoutToAddUnit;
        private AbsoluteLayout AbsoluteParent;
        private Button AdditionButton;

        private CodeBlock CodeBlock;
        public AssignmentAdditionButton(StackLayout layoutYoAddUnit, AbsoluteLayout absoluteParent, Button additionBUtton, CodeBlock codeBlock)
        {
            LayoutToAddUnit = layoutYoAddUnit;
            AbsoluteParent = absoluteParent;
            AdditionButton = additionBUtton;
            CodeBlock = codeBlock;
            assignmentAdditionButton = new Button()
            {
                Text = "Assignment",
                BackgroundColor = new Color(250 / 255.0, 128 / 255.0, 114 / 255.0),
                CornerRadius = 10,
            };
            assignmentAdditionButton.Clicked += AddAssignment;
        }
        private void AddAssignment(object sender, EventArgs e)
        {
            LayoutToAddUnit.Children.Remove(AdditionButton);
            LayoutToAddUnit.Children.Add(new AssignmentView(CodeBlock).GetAssignmentView());
            LayoutToAddUnit.Children.Add(AdditionButton);
            AbsoluteParent.Children.Remove((View)assignmentAdditionButton.Parent.Parent);
        }
        public Button GetAssignmentAdditionButton()
        {
            return assignmentAdditionButton;
        }
    }
}
