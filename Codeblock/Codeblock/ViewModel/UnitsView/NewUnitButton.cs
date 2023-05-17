using Codeblock.ViewModel.SelectionWindows;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Codeblock.Model;

namespace Codeblock.ViewModel.UnitsView
{
    public class NewUnitButton
    {
        private Button AdditionButton;
        private StackLayout stackLayout;
        private StackLayout LayoutToAddUnit;
        private AbsoluteLayout Parent;
        private Field Field;
        private bool IsMainBlock;
        private bool IsNodeBlock;

        private Node Node;
        private CodeBlock CodeBlock;
        public NewUnitButton(bool isMainBlock, StackLayout layoutToAddUnit, AbsoluteLayout parent, Field field, CodeBlock codeBlock, bool isNodeBlock = false, Node node = null)
        {
            LayoutToAddUnit = layoutToAddUnit;
            IsMainBlock = isMainBlock;
            IsNodeBlock = isNodeBlock;
            Parent = parent;
            Node = node;
            Field = field;
            CodeBlock = codeBlock;
            AdditionButton = new Button()
            {
                Text = "+",
                TextColor = Color.Black,
                FontSize = 20,
                BorderColor = Color.DarkViolet,
                BorderWidth = 2,
                CornerRadius = 10,
                WidthRequest = 50,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = new Color(0.0 / 255.0, 191 / 255.0, 6 / 255.0),
            };
            stackLayout = new StackLayout();
            AdditionButton.Clicked += OpenSelectionLayout;
            stackLayout.Children.Add(AdditionButton);
        }
        private void OpenSelectionLayout(object sender, EventArgs e)
        {
            Parent.Children.Add(new SelectionBar(IsMainBlock, LayoutToAddUnit, Parent, Field, AdditionButton, CodeBlock, IsNodeBlock, Node).GetSelectionBar(), new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
        }

        public StackLayout GetUnitBtn()
        {
            return stackLayout;
        }
    }
}
