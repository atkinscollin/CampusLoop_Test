using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.Share;
// Shows detailed event page after creation to a user

/* TODO: Add edit button for page owner
 *       Create popup page for choosing which social site to share to
 *       Add linkability - Links will give oppurtunity for people to share reg links n stuff
    */

namespace UEApp
{
    public partial class EventView : ContentPage
    {
        EventsViewModel vm;
        // items source for the picker to change time before event to get a reminder
        public List<string> attend_status = new List<string>
        {
            "Going",
            "Undecided",
            "Can't Go"
        };

        public EventView(string givenID)
        {
            InitializeComponent();

            vm = new EventsViewModel();
            vm._ID = givenID;
            vm.LoadSingleEventCommand.Execute(null);
            BindingContext = vm.SingleEvent;

            this.Title = "Event";

            var share_action = new Command(() 
                => CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                // Change to event page url
                Text = "Share event page",
                Title = "Share"
            }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_share_white_36dp.png", Command = share_action });

            var Goto_MainPage = new Command(() => Navigation.PushModalAsync(new MainPage() { Title="Home" }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_home_white_36dp.png", Command = Goto_MainPage });

            pickle.ItemsSource = attend_status;

            // Users do not have to enter a specific address, they can just enter a place
            //  like 'Baldwin Hall', so if there is no address we do not need the text from
            //  that entry or the navigation button on the page.
            bool Address_Non_Null = true;
            if (Address_Non_Null)
            {
                Place_Label.Text = "Kool Lounge";
                Address_Label.Text = "8750 Lane Dr. Cincinnati Ohio";
                Address_Label.IsEnabled = true;
                Address_Label.IsVisible = true;
                Nav_Button.IsEnabled = true;
                Nav_Button.IsVisible = true;
            }
            else
            {
                Place_Label.Text = "Kool Lounge";
            }
        }

        private void pickle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (pickle.SelectedIndex != -1)
            {
                Attend_Button.Text = pickle.Items[pickle.SelectedIndex];
            }
        }

        private void Attend_Button_Clicked(object sender, System.EventArgs e)
        {
            pickle.IsEnabled = true; pickle.Focus();
        }

        private void Attenders_Button_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Attenders button clicked", "Add popup window with list of attenders", "Cancel");
        }

        private void Nav_Button_Clicked(object sender, System.EventArgs e)
        {
            // TODO: Link to native navigation app
        }

        private void Cal_Button_Clicked(object sender, System.EventArgs e)
        {
            // TODO: Link to native navigation app
        }
    }
}