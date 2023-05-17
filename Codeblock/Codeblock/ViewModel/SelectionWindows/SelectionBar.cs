using Codeblock.ViewModel.SelectionWindows.SelectionBarButtons;
using Xamarin.Forms;
using Codeblock.Model;

namespace Codeblock.ViewModel.SelectionWindows
{
    public class SelectionBar
    {
        private StackLayout selectionBar;
        private StackLayout LayoutToAddUnit;
        private AbsoluteLayout AbsoluteParent;
        private Field Field;
        private Button AdditionButton;
        private bool IsMainBlock;
        private bool IsNodeBlock;
        public SelectionBar(bool isMainBlock, StackLayout layoutToAddUnit, AbsoluteLayout absoluteParent, Field field, Button additionButton, CodeBlock codeBlock, bool isNodeBlock = false, Node nodeBlock = null)
        {
            LayoutToAddUnit = layoutToAddUnit;
            AbsoluteParent = absoluteParent;
            Field = field;
            AdditionButton = additionButton;
            IsMainBlock = isMainBlock;
            IsNodeBlock = isNodeBlock;
            selectionBar = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = new Color(0.0 / 255.0, 8.0 / 255.0, 20.0 / 255.0),
            };
            ComposeSelectionBar(codeBlock, nodeBlock);
        }
        private void ComposeSelectionBar(CodeBlock codeBlock, Node node = null)
        {
            StackLayout st = new StackLayout()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            if (IsMainBlock)
            {
                st.Children.Add(new VariableAdditionButton(LayoutToAddUnit, AbsoluteParent, AdditionButton, codeBlock).GetVariableAdditionButton());
                st.Children.Add(new FunctionAdditionButton(LayoutToAddUnit, AbsoluteParent).GetFunctionAdditionButton());
                st.Children.Add(new CancelButton(AbsoluteParent).GetCancelButton());
            }
            else if (IsNodeBlock)
            {
                st.Children.Add(new ElifAdditionButton(LayoutToAddUnit, AbsoluteParent, AdditionButton, codeBlock, (LogicBlock)node).GetElifAdditionButton());
                st.Children.Add(new ElseAdditionButton(LayoutToAddUnit, AbsoluteParent, AdditionButton, codeBlock, (LogicBlock)node).GetElseAdditionButton());
                st.Children.Add(new CancelButton(AbsoluteParent).GetCancelButton());
            }
            else
            {
                st.Children.Add(new VariableAdditionButton(LayoutToAddUnit, AbsoluteParent, AdditionButton, codeBlock).GetVariableAdditionButton());
                st.Children.Add(new AssignmentAdditionButton(LayoutToAddUnit, AbsoluteParent, AdditionButton, codeBlock).GetAssignmentAdditionButton());
                st.Children.Add(new OutputBlockAdditionButton(LayoutToAddUnit, AbsoluteParent, AdditionButton, codeBlock).GetOutputBlockAdditionButton());
                st.Children.Add(new LogicBlockAdditionButton(LayoutToAddUnit, AbsoluteParent, Field, AdditionButton, codeBlock).GetLogicBlockAdditionButton());
                st.Children.Add(new CancelButton(AbsoluteParent).GetCancelButton());
            }

            selectionBar.Children.Add(st);
        }
        public StackLayout GetSelectionBar()
        {
            return selectionBar;
        }
    }
}
