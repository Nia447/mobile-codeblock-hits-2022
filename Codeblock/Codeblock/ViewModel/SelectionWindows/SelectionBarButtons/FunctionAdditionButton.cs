using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace Codeblock.ViewModel.SelectionWindows.SelectionBarButtons
{
    public class FunctionAdditionButton
    {
        private Button functionAdditionButton;
        private StackLayout LayoutToAddUnit;
        private AbsoluteLayout AbsoluteParent;
        public FunctionAdditionButton(StackLayout layoutToAddUnit, AbsoluteLayout absoluteParent)
        {
            LayoutToAddUnit = layoutToAddUnit;
            AbsoluteParent = absoluteParent;
            functionAdditionButton = new Button()
            {
                Text = "Function",
                BackgroundColor = new Color(255 / 255.0, 0, 102 / 255.0),
                CornerRadius = Helpers.Helper.Radius,
            };
            functionAdditionButton.Clicked += AddFucntion;
        }
        private void AddFucntion(object sender, EventArgs e)
        {
            AbsoluteParent.Children.Remove((View)functionAdditionButton.Parent.Parent);
        }
        public Button GetFunctionAdditionButton()
        {
            return functionAdditionButton;
        }
    }
}
