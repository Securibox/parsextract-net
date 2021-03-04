using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Securibox.ParseXtract.HttpHandler;
using Securibox.ParseXtract.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Securibox.ParseXtract
{
    /// <summary>
    /// Provides methods to call PX-Studio Parse API.
    /// </summary>
    public class Client
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="clientId">Px-Studio client ID</param>
        /// <param name="clientSecret">Px-Studio client secret</param>
        /// <param name="logger">Optional <see cref="Microsoft.Extensions.Logging.ILogger"/> logger</param>
        public Client(string clientId, string clientSecret, ILogger logger = null)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentNullException("clientId");
            }
            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new ArgumentNullException("clientSecret");
            }

            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Ignore,
            };

            HmacDelegatingHandler authorizationDelegateHandler = new HmacDelegatingHandler(clientId, clientSecret);
            if (logger != null)
            {
                var loggingHandler = new LoggingHandler(logger)
                {
                    InnerHandler = new HttpClientHandler()
                };
                authorizationDelegateHandler.InnerHandler = loggingHandler;
            }
            else
            {
                authorizationDelegateHandler.InnerHandler = new HttpClientHandler();
            }
            _httpClient = new HttpClient(authorizationDelegateHandler)
            {
                Timeout = new TimeSpan(0, 5, 0),
                BaseAddress = new Uri("https://px-api.securibox.eu")
            };
        }

        /// <summary>
        /// Calls the Parse API
        /// </summary>
        /// <param name="document">Document to be parsed</param>
        /// <returns>The parsing result</returns>
        public async Task<ParsingResult> ParseAsync(Document document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            using (var content = new StringContent(JsonConvert.SerializeObject(document, _serializerSettings), Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response = await _httpClient.PostAsync("api/parse", content))
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ParsingResult>(result, _serializerSettings);
                }
            }

        }
    }    
}
