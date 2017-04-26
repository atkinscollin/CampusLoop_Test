using System;

namespace UEApp
{
    public class Event
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string PhotoURL { get; set; }
        public string Tag1 { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay { get { return Date.ToLocalTime().ToString("dddd, MMMM dd"); } }

        [Newtonsoft.Json.JsonIgnore]
        public string TimeDisplay { get { return Date.ToLocalTime().ToString("hh:mm tt"); } }

        [Newtonsoft.Json.JsonIgnore]
        public string DateTimeDisplay { get { return Date.ToLocalTime().ToString("ddd, MMM dd hh:mm tt"); } }
    }
}