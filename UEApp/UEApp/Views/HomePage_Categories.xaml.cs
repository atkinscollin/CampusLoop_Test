using Xamarin.Forms;

// Home page tab with categories

namespace UEApp
{
    public partial class HomePage_Categories : ContentPage
    {
        EventsViewModel vm;
        public HomePage_Categories()
        {
            InitializeComponent();

            var Goto_SettingsPage = new Command(() => Navigation.PushModalAsync(new NavigationPage(new SettingsPage()) { Title = "Settings" }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_settings_white_36dp.png", Command = Goto_SettingsPage });

            var Goto_SearchPage = new Command(() => Navigation.PushModalAsync(new SearchPage()));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_search_white_36dp.png", Command = Goto_SearchPage });

            this.BindingContext = vm = new EventsViewModel();
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            Navigation.PushModalAsync(new NavigationPage(new SingleCategoryPage(e.SelectedItem.ToString())));
            ((ListView)sender).SelectedItem = null;
        }
    }
}
