using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gravitas.Infrastructure.Common.Configuration;
using Newtonsoft.Json;
using NLog;
using Polly;

namespace Gravitas.Infrastructure.Platform.ApiClient
{
    public class BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public BaseApiClient(string uriString)
        {
            _httpClient = new HttpClient(new RetryHandler(new HttpClientHandler()))
            {
                BaseAddress = new Uri(uriString), Timeout = TimeSpan.FromSeconds(120)
            };
        }

        protected Uri BaseAddress => _httpClient.BaseAddress;

        public HttpResponseMessage Send(HttpRequestMessage requestMessage, int? timeoutInSeconds=120)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                _logger.Info($"Infrastructure. Send: Start Api request with message: {requestMessage.RequestUri} : {JsonConvert.SerializeObject(requestMessage.Content)}");
                var cred = GlobalConfigurationManager.OneCApiLogin + ":" + GlobalConfigurationManager.OneCApiPassword;
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(cred));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                var sw = new Stopwatch();
                sw.Start();
                responseMessage = _httpClient.SendAsync(requestMessage).GetAwaiter().GetResult();
                sw.Stop();
                _logger.Info($"OneC api time processing: {requestMessage.RequestUri} = {sw.Elapsed}");
            }
            catch (Exception ex)
            {
                _logger.Error($"Infrastructure. Send: Error while sending Api request: {ex.Message}: {ex.StackTrace}");
            }

            return responseMessage;
        }
    }

    internal class RetryHandler : DelegatingHandler
    {
        private const int RetriesCount = 3;

        public RetryHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.BadGateway)
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(RetriesCount, retryAttempt => TimeSpan.FromMilliseconds(retryAttempt * 500))
                .ExecuteAsync(() => base.SendAsync(request, cancellationToken));
        }
    }
}