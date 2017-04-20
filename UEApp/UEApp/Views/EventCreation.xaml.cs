using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

/* Popup page for creating a new event, accessed through plus button on main menu
   TODO: Add extended entry for an about
         Temp fix for length of tags in, fix width being autoupdated so long words arent chopped off
         Add default banner pick
*/

namespace UEApp
{
    public partial class EventCreation : PopupPage
    {
        public EventCreation()
        {
            InitializeComponent();

            //BindingContext = new EventsDataModel();

            timepickle.Time = new TimeSpan(DateTime.Now.Hour + 1, 0, 0);
        }

        private async void OnImageSelect(object sender, EventArgs e)
        {
            // Code for enabling user to select banner commented out at bottom
            var gallery_view_page = new GalleryView();
            await Navigation.PushPopupAsync(gallery_view_page);
        }

        private void OnCancel(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        // Checks a lot of things to see if they are okay, if everything passes it pops the page
        private void OnSubmit(object sender, EventArgs e)
        {
            int flag = 0;
            
            // Cleans out past warnings
            Warn_Layout.Children.Clear();

            // Checks if any of the fields are empty; if they are warns the user
            if (String.IsNullOrEmpty(Event_Title.Text) || String.IsNullOrEmpty(Event_Place.Text))
            {
                if (String.IsNullOrEmpty(Event_Title.Text))
                {
                    Label Warn_Label = new Label { Text = "Please enter a Title for the event", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                }
                if (String.IsNullOrEmpty(Event_Place.Text))
                {
                    Label Warn_Label = new Label { Text = "Please enter an Place for the event", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                }
                flag = 1;
            }

            // Checks to make sure the date and time picked are in the future
            if (DateTime.Now.Day.CompareTo(datepickle.Date.Day) == 0)
            {
                if (timepickle.Time.Hours < DateTime.Now.Hour)
                {
                    Label Warn_Label = new Label { Text = "Please enter a time in the future", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                    flag = 1;
                }
                else if ((timepickle.Time.Hours == DateTime.Now.Hour) && (timepickle.Time.Minutes < DateTime.Now.Minute))
                {
                    Label Warn_Label = new Label { Text = "Please enter a time in the future", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                    flag = 1;
                }
            }

            // Checks to see if any tags are dupes, if any found it gets rid of em
            if (pickle0.SelectedIndex == pickle1.SelectedIndex || pickle0.SelectedIndex == pickle2.SelectedIndex ||
                pickle1.SelectedIndex == pickle2.SelectedIndex)
            {
                if (pickle0.SelectedIndex == pickle1.SelectedIndex && pickle0.SelectedIndex == pickle2.SelectedIndex)
                {
                    pickle1.SelectedIndex = -1;
                    pickle2.SelectedIndex = -1;
                    pickle2.IsVisible = false;
                }
                else if (pickle0.SelectedIndex == pickle1.SelectedIndex && pickle2.SelectedIndex == -1)
                {
                    pickle1.SelectedIndex = -1;
                    pickle2.IsVisible = false;
                }
                else if (pickle0.SelectedIndex == pickle1.SelectedIndex && pickle2.SelectedIndex != -1)
                {
                    pickle1.SelectedIndex = pickle2.SelectedIndex;
                    pickle2.SelectedIndex = -1;
                }
                else if (pickle0.SelectedIndex == pickle2.SelectedIndex)
                {
                    pickle2.SelectedIndex = -1;
                }
                else if (pickle1.SelectedIndex == pickle2.SelectedIndex)
                {
                    pickle2.SelectedIndex = -1;
                }
            }

            if(flag == 1)
            {
                Warn_Layout.IsVisible = true;
            }
            else
            {
                //var Event = new EventsDataModel.Event { Title = Event_Title.Text, Location = Event_Place.Text };
                PopupNavigation.PopAsync();
            }
        }

        private void CategoryPicked(object sender, System.EventArgs e)
        {
            Picker pick = (Picker)sender;

            if (pick.Title.Contains("Tag #1") && pickle1.IsVisible == false)
            {
                pickle1.IsVisible = true;
            }

            if (pick.Title.Contains("Tag #2") && pickle2.IsVisible == false)
            {
                pickle2.IsVisible = true;
            }
        }
    }
}

/*
            This is for letting users select an their own banner
            Plugins.Media package is needed

            This goes in AssembyInfo.cs for Droid to know that it can access it
            [assembly: UsesFeature("android.hardware.camera", Required = false)]
            [assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]

            Also needed for Droid in MainActivity.cs
            public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
            {
                PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }

            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Photo Picker", "No Pick photo available :(", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null) return;

            Create file file_paths.xaml in Resources
            <?xml version="1.0" encoding="utf-8"?>
            <paths xmlns:android="http://schemas.android.com/apk/res/android">
                <external-path name="my_images" path="Android/data/UEApp/files/Pictures" />
                <external-path name="my_movies" path="Android/data/UEApp/files/Movies" />
            </paths>
*/
