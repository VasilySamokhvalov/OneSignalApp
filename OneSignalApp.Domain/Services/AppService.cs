using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OneSignalApp.Domain.Models.AppModels;
using OneSignalApp.Domain.Options;
using OneSignalApp.Domain.Services.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneSignalApp.Domain.Services
{
    public class AppService : IAppService
    {
        private readonly IOptions<OneSignalOptions> _oneSignalAuthOptions;
        private RestClient Client { get; set; }
        public AppService(IOptions<OneSignalOptions> oneSignalAuthOptions)
        {
            _oneSignalAuthOptions = oneSignalAuthOptions;

            Client = new RestClient(_oneSignalAuthOptions.Value.APILink);
        }

        public async Task<List<App>> GetApps()
        {
            var request = CreateRequest();
            var response = Client.Get(request);
            var content = JsonConvert.DeserializeObject<List<App>>(response.Content);
            return content;
        }

        public async Task<App> GetApp(string id)
        {
            var request = CreateRequest(id);
            request.AddHeader("Content-Type", "application/json");
            var response = Client.Get(request);
            var content = JsonConvert.DeserializeObject<App>(response.Content);
            return content;
        }

        public async Task<bool> UpdateApp(string id, UpdateAppModel model)
        {
            var request = CreateRequest(id);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(JsonConvert.SerializeObject(model));
            var response = Client.Put(request);
            return response.IsSuccessful;
        }

        public async Task<bool> CreateApp(CreateAppModel model)
        {
            var request = CreateRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(JsonConvert.SerializeObject(model));
            var response = Client.Post(request);
            return response.IsSuccessful;
        }

        private RestRequest CreateRequest(string id = null)
        {
            RestRequest request = new RestRequest();
            if (string.IsNullOrEmpty(id))
            {
                request = new RestRequest("apps", Method.GET);
                request.AddHeader("Authorization", "Basic " + _oneSignalAuthOptions.Value.AuthKey);
                return request;
            }
            request = new RestRequest("apps/" + id, Method.POST);
            request.AddHeader("Authorization", "Basic " + _oneSignalAuthOptions.Value.AuthKey);
            return request;
        }
    }
}
