using System;
using Xamarin.Forms;

/* Page with events from a single category
 * Accessed from category tab
 */

namespace UEApp
{
    public partial class SingleCategoryPage : ContentPage
    {
        public SingleCategoryPage(String CategoryTitle)
        {
            InitializeComponent();
            //list.ItemsSource = EventsDataModel.All;

            this.Title = CategoryTitle;

            var Goto_MainPage = new Command(() => Navigation.PushModalAsync(new MainPage() { Title = "Home" }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_home_white_36dp.png", Command = Goto_MainPage });
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

        // Using this temporarily to look at the eventview page
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            Navigation.PushModalAsync(new NavigationPage(new EventView()) { Title = "Event" });
            ((ListView)sender).SelectedItem = null;
        }
    }
}