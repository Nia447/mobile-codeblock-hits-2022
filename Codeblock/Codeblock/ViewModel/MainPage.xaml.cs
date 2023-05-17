using Codeblock.ViewModel;
using Xamarin.Forms;

namespace Codeblock
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            MainContent mainContent = new MainContent();
            Content = mainContent.GetMainContent();
        }
    }
}
