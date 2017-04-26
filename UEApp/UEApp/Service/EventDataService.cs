using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(UEApp.EventDataService))]
namespace UEApp
{
    public class EventDataService
    {
        public MobileServiceClient MobileService { get; set; } = null;
        IMobileServiceSyncTable<Event> eventTable;
        public static bool UseAuth { get; set; } = false;

        public async Task Initialize()
        {
            if (MobileService?.SyncContext?.IsInitialized ?? false)
                return;

            var appUrl = "http://campusloopservice.azurewebsites.net";

#if AUTH
            MobileService = new MobileServiceClient(appUrl, new AuthHandler());

            if (!string.IsNullOrWhiteSpace (Settings.AuthToken) && !string.IsNullOrWhiteSpace (Settings.UserId)) {
                MobileService.CurrentUser = new MobileServiceUser (Settings.UserId);
                MobileService.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
            }
#else
            //Create our client
            MobileService = new MobileServiceClient(appUrl);

#endif

            // Init DB path
            string path = "syncstore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Event>();

            await MobileService.SyncContext.InitializeAsync(store);

            //Get our sync table that will call out to azure
            eventTable = MobileService.GetSyncTable<Event>();
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            await Initialize();
            await SyncEvent();

            return await eventTable.OrderBy(c => c.Date).ToEnumerableAsync();
        }

        public async Task<Event> GetEvent(string givenID)
        {
            await Initialize();
            await SyncEvent();

            return await eventTable.LookupAsync(givenID);
        }

        public async Task<Event> AddEvent(string title, string location, DateTime date, string photoURL, string tag1)
        {
            await Initialize();

            //create and insert new event
            var freshEvent = new Event { Title = title, Location = location, Date = date, PhotoURL = photoURL, Tag1 = tag1 };

            await eventTable.InsertAsync(freshEvent);

            //Synchronize event
            await SyncEvent();

            return freshEvent;
        }

        public async Task SyncEvent()
        {
            try
            {
                //pull down all latest changes and then push current events up
                await eventTable.PullAsync("allEvents", eventTable.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync events, we are offline: " + ex);
            }
        }

        public async Task<bool> LoginAsync()
        {
            
            await Initialize();

            /*var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(MobileService, MobileServiceAuthenticationProvider.MicrosoftAccount);

            if (user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Login Error", "Unable to login, please try again", "OK");
                });
                return false;
            }
            else
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;
            }

            return true;
            */
            
            return false;
        }
    }
}