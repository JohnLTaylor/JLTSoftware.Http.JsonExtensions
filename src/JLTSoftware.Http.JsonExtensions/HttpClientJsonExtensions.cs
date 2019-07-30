using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace System.Net.Http.JsonExtensions
{
    public static class HttpClientJsonExtensions
    {
        private const string MediaType = "application/json";

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T value)
        {
            return PostAsJsonAsync(httpClient, requestUri, value, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T value, CancellationToken cancellationToken)
        {
            return PostAsJsonAsync(httpClient, new Uri(requestUri), value, cancellationToken);
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T value)
        {
            return PostAsJsonAsync(httpClient, requestUri, value, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T value, CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, MediaType);
            return httpClient.PostAsync(requestUri, content, cancellationToken);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T value)
        {
            return PutAsJsonAsync(httpClient, requestUri, value, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T value, CancellationToken cancellationToken)
        {
            return PutAsJsonAsync(httpClient, new Uri(requestUri), value, cancellationToken);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T value)
        {
            return PutAsJsonAsync(httpClient, requestUri, value, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T value, CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, MediaType);
            return httpClient.PutAsync(requestUri, content, cancellationToken);
        }

        public static Task<T> GetAsJsonAsync<T>(this HttpClient httpClient, string requestUri)
        {
            return GetAsJsonAsync<T>(httpClient, requestUri, CancellationToken.None);
        }

        public static Task<T> GetAsJsonAsync<T>(this HttpClient httpClient, string requestUri, CancellationToken cancellationToken)
        {
            return GetAsJsonAsync<T>(httpClient, new Uri(requestUri), cancellationToken);
        }

        public static Task<T> GetAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri)
        {
            return GetAsJsonAsync<T>(httpClient, requestUri, CancellationToken.None);
        }

        public static async Task<T> GetAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, CancellationToken cancellationToken)
        {
            using (var responseMessage = await httpClient.GetAsync(requestUri, cancellationToken))
            using (var content = responseMessage.EnsureSuccessStatusCode().Content)
            {
                var stringValue = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(stringValue);
            }

        }
    }
}
