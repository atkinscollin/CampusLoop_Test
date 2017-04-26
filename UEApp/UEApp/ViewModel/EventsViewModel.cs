using System;
using MvvmHelpers;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace UEApp
{
    public class EventsViewModel : BaseViewModel
    {
        EventDataService eventDataService;
        public EventsViewModel()
        {
            eventDataService = DependencyService.Get<EventDataService>();
        }

        public ObservableRangeCollection<Event> Events { get; } = new ObservableRangeCollection<Event>();
        
        public List<string> categories = new List<string>
        {
            "Art",
            "Business",
            "Casual",
            "Food",
            "Fun",
            "Literature",
            "Math",
            "Meeting",
            "Music",
            "Science",
            "Sports",
            "Technology"
        };

        public List<string> Categories => categories;

        string loadingMessage;
        public string LoadingMessage
        {
            get { return loadingMessage; }
            set { SetProperty(ref loadingMessage, value); }
        }

        ICommand loadEventsCommand;
        public ICommand LoadEventsCommand =>
            loadEventsCommand ?? (loadEventsCommand = new Command(async () => await ExecuteLoadEventsCommandAsync()));

        async Task ExecuteLoadEventsCommandAsync()
        {
            if (IsBusy || !(await LoginAsync()))
                return;

            try
            {
                LoadingMessage = "Loading Events...";
                IsBusy = true;
                var newEvents = await eventDataService.GetEvents();
                Events.ReplaceRange(newEvents);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);

                await Application.Current.MainPage.DisplayAlert("Sync Error", "Unable to sync events, you may be offline", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        string _title;
        public string _Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        string _location;
        public string _Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        DateTime _date;
        public DateTime _Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        string _photoURL;
        public string _PhotoURL
        {
            get { return _photoURL; }
            set { SetProperty(ref _photoURL, value); }
        }

        string _tag1;
        public string _Tag1
        {
            get { return _tag1; }
            set { SetProperty(ref _tag1, value); }
        }

        ICommand addEventCommand;
        public ICommand AddEventCommand =>
            addEventCommand ?? (addEventCommand = new Command(async () => await ExecuteAddEventCommandAsync()));

        async Task ExecuteAddEventCommandAsync()
        {
            if (IsBusy || !(await LoginAsync()))
                return;

            try
            {
                LoadingMessage = "Adding Event...";
                IsBusy = true;

                var freshEvent = await eventDataService.AddEvent(_Title, _Location, _Date, _PhotoURL, _Tag1);
                Events.Add(freshEvent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);
            }
            finally
            {
                LoadingMessage = string.Empty;
                IsBusy = false;
            }
        }

        public Task<bool> LoginAsync()
        {
            if (Settings.IsLoggedIn)
                return Task.FromResult(true);

            return eventDataService.LoginAsync();
        }

    }
}

