using Newtonsoft.Json;

namespace OneSignalApp.Domain.Models.AppModels
{
    public class UpdateAppModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
