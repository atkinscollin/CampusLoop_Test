using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace UEApp
{
    public class EventDataService
    {
        public MobileServiceClient MobileService { get; set; } = null;
        IMobileServiceSyncTable<Event> eventTable;

        public async Task Initialize()
        {
            if (MobileService?.SyncContext?.IsInitialized ?? false)
                return;

            //Create our client
            MobileService = new MobileServiceClient("http://campusloopservice.azurewebsites.net");

            const string path = "syncstore.db";
            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Event>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            eventTable = MobileService.GetSyncTable<Event>();
        }

        public async Task<IEnumerable> GetEvents()
        {
            await Initialize();
            await SyncEvent();
            return await eventTable.OrderBy(c => c.Date).ToEnumerableAsync();
        }

        public async Task<Event> AddEvent(string title, string location, DateTime date, string photoURL, string tag1)
        {
            await Initialize();

            //create and insert coffee
            var freshEvent = new Event { Title = title, Location = location, Date = date, PhotoURL = photoURL, Tag1 = tag1 };

            await eventTable.InsertAsync(freshEvent);

            //Synchronize coffee
            await SyncEvent();

            return freshEvent;
        }

        public async Task SyncEvent()
        {
            //pull down all latest changes and then push current coffees up
            await eventTable.PullAsync("allEvents", eventTable.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }
    }
}