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
        public string DateDisplay { get { return Date.ToLocalTime().ToString("d"); } }

        [Newtonsoft.Json.JsonIgnore]
        public string TimeDisplay { get { return Date.ToLocalTime().ToString("t"); } }
    }
}