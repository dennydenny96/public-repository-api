using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace bopg.api.account.Helper
{
    public class APICallHelper
    {
        #region -= Properties =-
        public IHttpClientFactory HttpClientFactory { get; set; }
        #endregion

        #region -= Contructor =-
        public APICallHelper(IHttpClientFactory httpClientFactory)
        {
            this.HttpClientFactory = httpClientFactory;
        }
        #endregion

        #region -= Private Methods =-
        private T HttpClient<T, U>(ref Exception exception, string url, U data)
        {
            T retVal = default(T);
            var client = this.HttpClientFactory.CreateClient("BAS");
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            try
            {
                var result = client.PostAsync(url, content).Result;

                if (result.IsSuccessStatusCode)
                {
                    var received = result.Content.ReadAsStringAsync().Result;

                    retVal = JsonConvert.DeserializeObject<T>(received);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return retVal;
        }
        #endregion

    }
}
