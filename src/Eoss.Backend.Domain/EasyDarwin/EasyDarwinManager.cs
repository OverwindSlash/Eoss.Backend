using Abp.Domain.Services;
using Microsoft.AspNetCore.Hosting;

namespace Eoss.Backend.Domain.EasyDarwin
{
    public class EasyDarwinManager : DomainService, IEasyDarwinManager
    {
        private readonly EasyDarwinRestService _edRestService;

        public EasyDarwinManager(IWebHostEnvironment env)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _edRestService = new EasyDarwinRestService(new HttpClient(clientHandler), env);
        }

        public async Task<string> StartStreaming(string url, string path, string transType, int timeoutSec, int heartbeatIntervalSec)
        {
            var pushers = await _edRestService.GetAllPushers();

            var existPusher = pushers.FirstOrDefault(pusher => pusher.source == url);
            if (existPusher != null)
            {
                return existPusher.id;

                //await _edRestService.StopStreaming(existPusher.id);
            }
            else
            {
                return await _edRestService.StartStreaming(url, path, transType, timeoutSec, heartbeatIntervalSec);
            }
        }

        public async Task<string> GetStreamingUri(string streamingId)
        {
            var pushers = await _edRestService.GetAllPushers();

            var existPusher = pushers.FirstOrDefault(pusher => pusher.id == streamingId);
            if (existPusher != null)
            {
                return existPusher.url;
            }

            return string.Empty;
        }

        public async Task StopStreaming(string streamingId)
        {
            await _edRestService.StopStreaming(streamingId);
        }
    }
}
