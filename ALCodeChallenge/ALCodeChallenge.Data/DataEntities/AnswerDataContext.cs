using ALCodeChallenge.Data.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ALCodeChallenge.Data.DataEntities
{
    public class AnswerDataContext : IAnswerDataContext
    {
        public async Task<string> GetAnswersAsync(int questionId)
        {
            // Uri generated on Stack Overflow's api documentation site. Includes filtering to limit the fields returned.
            var requestUri = $"https://api.stackexchange.com/2.2/questions/{questionId}/answers?pagesize=100&order=desc&sort=activity&site=stackoverflow&filter=!)sBhRh8NkgpEppLV((Hu";

            // Handler required to decompress GZIP responses from api. 
            var handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            try
            {
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.BaseAddress = new Uri(requestUri);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    return await httpClient.GetStringAsync(requestUri);
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
    }
}
