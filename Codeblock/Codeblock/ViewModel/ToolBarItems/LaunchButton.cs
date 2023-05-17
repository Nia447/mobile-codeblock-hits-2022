using Xamarin.Forms;
using System;

namespace Codeblock.ViewModel.ToolBarItems
{
    public class LaunchButton
    {
        Button launchButton;
        Field Field;
        public LaunchButton(Field field)
        {
            launchButton = new Button()
            {
                BackgroundColor = new Color(0.0 / 255.0, 191 / 255.0, 6 / 255.0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Launch",
                FontSize = 19,
                BorderColor = Color.BlueViolet,
                BorderWidth = 2,
                CornerRadius = 20,
                TextColor = Color.Black,
                WidthRequest = 120,
                Margin = 10,
            };
            Field = field;
            launchButton.Clicked += LaunchCompilation;
        }
        public void LaunchCompilation(object sender, EventArgs e)
        {
            Field.MainBlockView.ElderCodeBlock.StartCompilation();
        }
        public Button GetLaunchButton()
        {
            return launchButton;
        }
    }
}
