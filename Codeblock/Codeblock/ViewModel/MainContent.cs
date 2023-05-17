using Codeblock.ViewModel.ToolBarItems;
using Xamarin.Forms;
namespace Codeblock.ViewModel
{
    public class MainContent
    {
        private StackLayout mainContent;
        private Field Field;
        private ToolBar ToolBar;
        public MainContent()
        {            
            mainContent = new StackLayout()
            {
                Spacing = 0,
            };
            Field = new Field();
            ToolBar = new ToolBar(Field);

            ComposeMainContent();
        }
        private void ComposeMainContent()
        {
            mainContent.Children.Add(Field.GetField());
            mainContent.Children.Add(ToolBar.GetToolBar());
        }
        public StackLayout GetMainContent()
        {
            return mainContent;
        }
    }
}
