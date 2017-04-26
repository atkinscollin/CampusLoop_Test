using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;

/* Main homepage of the app, first tab
   Shows the feed of most recent/upcoming events
 */

namespace UEApp
{
    public partial class HomePage_Events : ContentPage
    {
        EventsViewModel vm;
        public HomePage_Events()
        {
            InitializeComponent();

            BindingContext = vm = new EventsViewModel();

            var Goto_SettingsPage = new Command(() => Navigation.PushModalAsync(new NavigationPage(new SettingsPage()) { Title = "Settings" }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_settings_white_36dp.png", Command = Goto_SettingsPage });

            var Goto_SearchPage = new Command(() => Navigation.PushModalAsync(new SearchPage()));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_search_white_36dp.png", Command = Goto_SearchPage });
        }

        protected override void OnAppearing()
        {
            // Clears last page from memory after home button has been pressed
            // await Navigation.PopModalAsync();

            base.OnAppearing();

            if (vm.Events.Count == 0)
            {
                vm.LoadEventsCommand.Execute(null);
            }

            /*
            if (vm.Events.Count == 0 && Settings.IsLoggedIn)
                vm.LoadEventsCommand.Execute(null);
            else
            {
                await vm.LoginAsync();
                if (Settings.IsLoggedIn)
                    vm.LoadEventsCommand.Execute(null);
            }
            */
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }        

        // Custom function for displaying different background colors on alternating cells
        private bool isRowEven;
        private void Alt_Cell_Colors(object sender, EventArgs e)
        {
            if (this.isRowEven)
            {
                var viewCell = (ViewCell)sender;
                if (viewCell.View != null)
                {
                    viewCell.View.BackgroundColor = Color.FromHex("F5F5F5");
                }
            }
            this.isRowEven = !this.isRowEven;
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(Object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Selected", ((Event)e.SelectedItem).Id, "OK");
            await Navigation.PushModalAsync(new NavigationPage(new EventView(((Event)e.SelectedItem).Id)));

            ((ListView)sender).SelectedItem = null;
        }

        private async void Handle_FabClicked(object sender, EventArgs e)
        {
            var event_create_page = new EventCreation();
            event_create_page.CreateComplete += HandleCreateComplete;
            event_create_page.CreateIncomplete += HandleCreateIncomplete;
            await Navigation.PushPopupAsync(event_create_page);
        }

        private async void HandleCreateComplete(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }

        private async void HandleCreateIncomplete(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}