using Eoss.Backend.Domain.RestService;
using System.Collections.ObjectModel;
using Eoss.Backend.Entities;
using Mictlanix.DotNet.Onvif.Common;
using Mictlanix.DotNet.Onvif.Device;

namespace Eoss.Backend.Domain.EasyDarwin
{
    public class EasyDarwinRestService : RestServiceBase
    {
        private const string UrlPrefix = @"http://192.168.1.40:10008/";

        public EasyDarwinRestService(HttpClient httpClient) : base(httpClient)
        {
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
