using LogConverter.Data.Interfaces;
using System.Net;

namespace LogConverter.Data.Services
{
    public class LogReaderService : ILogReaderService
    {
        private readonly WebClient _webClient;

        public LogReaderService(WebClient webClient)
        {
            _webClient = webClient;
        }

        public string ReadFileContentFromUrl(string url)
            => _webClient.DownloadString(url);
    }
}
