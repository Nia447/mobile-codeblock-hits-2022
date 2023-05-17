using Codeblock.ViewModel.ToolBarItems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Codeblock.ViewModel.ToolBarItems
{
    public class ToolBar
    {
        private StackLayout toolbar;
        public ToolBar(Field field)
        {            
            toolbar = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = new Color(13.0 / 255.0, 53 / 255.0, 116 / 255.0),
                HeightRequest = 80,
            };
            ComposeToolBar(field);
        }
        private void ComposeToolBar(Field field)
        {
            toolbar.Children.Add(new LaunchButton(field).GetLaunchButton());
        }
        public StackLayout GetToolBar()
        {
            return toolbar;
        }
    }
}