using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HH
{
    class Connect
    {
        public string proxyUri = "194.226.128.245:3128";
        public string proxyName = "user4";
        public string proxyPass = "user4";
        private NetworkCredential proxyCredential;
        private WebProxy proxy;
        private HttpClient client;
        private HttpClientHandler httpClientHandler;

        /// <summary>
        /// Создать HttpClient для работы с API
        /// </summary>
        /// <param name="useProxy"> Использование прокси</param>
        /// <returns>HttpClient</returns>
        public HttpClient CreateConnect(bool useProxy)
        {
            proxyCredential = new NetworkCredential(proxyName, proxyPass);
            proxy = new WebProxy(proxyUri, false)
            {
                UseDefaultCredentials = false,
                Credentials = proxyCredential,
            };
            client = null;
            httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy,
                PreAuthenticate = true,
                UseDefaultCredentials = false,
            };
            httpClientHandler.Credentials = proxyCredential;
            client = useProxy ? new HttpClient(httpClientHandler): new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("test-api", "1"));
            return client;
        }
    }
}
