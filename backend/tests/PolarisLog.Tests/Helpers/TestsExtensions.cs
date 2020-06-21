using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace PolarisLog.Tests.Helpers
{
    public static class TestsExtensions
    {
        public static void AtribuirToken(this HttpClient httpClient, string token)
        {
            httpClient.AtribuirJsonMediaType();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static void AtribuirJsonMediaType(this HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        }
    }
}