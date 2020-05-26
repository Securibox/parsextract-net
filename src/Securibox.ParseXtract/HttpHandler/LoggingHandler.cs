using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Securibox.ParseXtract.HttpHandler
{
    class LoggingHandler : DelegatingHandler
    {
        ILogger _logger;
        public LoggingHandler(ILogger logger)
        {
            _logger = logger;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            StringBuilder requestTrace = new StringBuilder();
            requestTrace.AppendLine("--- REQUEST ---");
            requestTrace.AppendLine(request.ToString());
            if (request.Content != null)
            {
                string content = await request.Content.ReadAsStringAsync();
                requestTrace.AppendLine(StripContent(content));

            }
            requestTrace.AppendLine();
            _logger.LogInformation(requestTrace.ToString());

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            requestTrace = new StringBuilder();
            requestTrace.AppendLine("--- RESPONSE ---");
            requestTrace.AppendLine(response.ToString());
            if (response.Content != null)
            {
                requestTrace.AppendLine(await response.Content.ReadAsStringAsync());
            }
            requestTrace.AppendLine();
            _logger.LogInformation(requestTrace.ToString());

            return response;
        }

        private string StripContent(string content)
        {
            try
            {
                var bodyContent = JToken.Parse(content);
                var contentTokens = bodyContent.SelectTokens("[*].['content']");
                if (contentTokens != null)
                {
                    foreach (var contentToken in contentTokens)
                    {
                        var jProperty = contentToken.Parent as JProperty;
                        if (jProperty != null)
                        {
                            jProperty.Value = contentToken.ToString().Substring(0, 10) + "...";
                        }
                    }
                }
                return JsonConvert.SerializeObject(bodyContent, Formatting.Indented);
            }
            catch
            {
                return content;
            }
        }
    }
}
