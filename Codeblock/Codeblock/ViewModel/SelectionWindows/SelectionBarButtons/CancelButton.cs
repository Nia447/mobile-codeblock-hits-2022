using Xamarin.Forms;

namespace Codeblock.ViewModel.SelectionWindows.SelectionBarButtons
{
    public class CancelButton
    {
        private Button cancelButton;
        public CancelButton(AbsoluteLayout AbsoluteParent)
        {
            cancelButton = new Button()
            {
                BackgroundColor = Color.Gray,
                Text = "Cancel",
                CornerRadius = Helpers.Helper.Radius,
            };
            cancelButton.Clicked += (sender, e) =>
            {
                AbsoluteParent.Children.Remove((View)cancelButton.Parent.Parent);
            };
        }
        public Button GetCancelButton()
        {
            return cancelButton;
        }
    }
}
