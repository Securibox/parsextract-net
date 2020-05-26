using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Securibox.ParseXtract.HttpHandler
{/// <summary>
/// Handler to calculate the message signature and add it to the authorization HTTP message.
/// </summary>
    public class HmacDelegatingHandler : DelegatingHandler
    {
        private string _clientId;
        private string _clientSecret;
        /// <summary>
        /// Initializes a new instance of the <see cref="HmacDelegatingHandler"/> class.
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client Secret</param>
        public HmacDelegatingHandler(string clientId, string clientSecret)
            : base()
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }
        /// <summary>
        /// Sends the request with the HMAC signature in Authorization HTTP header.
        /// </summary>
        /// <param name="request">The request to be sent</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Parse response</returns>
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string requestContentBase64String = string.Empty;
            string requestUri = System.Uri.EscapeUriString(request.RequestUri.AbsoluteUri.ToLower());
            string requestHttpMethod = request.Method.Method;
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();
            Guid randomNonceGuid = Guid.Empty;
            using(var rng = new RNGCryptoServiceProvider())
            {
                var data = new byte[16];
                rng.GetBytes(data);
                randomNonceGuid = new Guid(data);
            }

            string nonce = randomNonceGuid.ToString("N");
            if (request.Content != null)
            {
                byte[] content = await request.Content.ReadAsByteArrayAsync();
                MD5 md5 = MD5.Create();
                byte[] requestContentHash = md5.ComputeHash(content);
                requestContentBase64String = Convert.ToBase64String(requestContentHash);
            }
            string signatureRawData = String.Format("{0}{1}{2}{3}{4}{5}", _clientId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);
            var secretKeyByteArray = Convert.FromBase64String(_clientSecret);
            byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);
            using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
            {
                byte[] signatureBytes = hmac.ComputeHash(signature);
                string requestSignatureBase64String = Convert.ToBase64String(signatureBytes);
                request.Headers.Authorization = new AuthenticationHeaderValue("hmacauth", string.Format("{0}:{1}:{2}:{3}", _clientId, requestSignatureBase64String, nonce, requestTimeStamp));
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
