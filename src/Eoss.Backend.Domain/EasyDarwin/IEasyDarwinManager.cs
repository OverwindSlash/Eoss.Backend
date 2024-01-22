using Abp.Domain.Services;

namespace Eoss.Backend.Domain.EasyDarwin
{
    public interface IEasyDarwinManager : IDomainService
    {
        Task<string> StartStreaming(string url, string path, string transType, 
            int timeoutSec, int heartbeatIntervalSec);

        Task<string> GetStreamingUri(string streamingId);

        Task StopStreaming(string streamingId);
    }
}
