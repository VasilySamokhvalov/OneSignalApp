using Newtonsoft.Json;

namespace OneSignalApp.Domain.Models.AppModels
{
    public class CreateAppModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
