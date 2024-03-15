using Eoss.Backend.Configuration;
using Eoss.Backend.Domain.RestService;
using Eoss.Backend.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;

namespace Eoss.Backend.Domain.EasyDarwin
{
    public class EasyDarwinRestService : RestServiceBase
    {
        private readonly string UrlPrefix;
        
        public EasyDarwinRestService(HttpClient httpClient, IWebHostEnvironment env) 
            : base(httpClient)
        {
            var _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());

            UrlPrefix = _appConfiguration["EasyDarwin:Address"];
        }

        internal async Task<ObservableCollection<EasyDarwinPusher>> GetAllPushers()
        {
            var pushers = new ObservableCollection<EasyDarwinPusher>();

            string restUrl = UrlPrefix + @"api/v1/pushers";
            var result = await GetObjectAsync<EasyDarwinGetPusherResponse>(restUrl);

            if (result != null)
            {
                foreach (var pusher in result.rows)
                {
                    pushers.Add(pusher);
                }
            }

            return pushers;
        }

        internal async Task<string> StartStreaming(string url, string path, string transType,
            int timeoutSec, int heartbeatIntervalSec)
        {
            url = url.Replace("&", "%26");

            string restUrl = UrlPrefix + $@"api/v1/stream/start?" +
            $"url={url}&customPath={path}&transType={transType}&idleTimeout={timeoutSec}&heartbeatInterval={heartbeatIntervalSec}";
            
            var result = await GetObjectAsync<string>(restUrl);

            return result;
        }

        internal async Task StopStreaming(string streamingId)
        {
            string restUrl = UrlPrefix + $@"api/v1/stream/stop?id={streamingId}";

            await GetObjectAsync(restUrl);
        }
    }
}
