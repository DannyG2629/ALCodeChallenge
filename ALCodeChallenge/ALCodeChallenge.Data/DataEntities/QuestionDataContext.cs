using ALCodeChallenge.Data.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;


namespace ALCodeChallenge.Data.DataEntities
{
    public class QuestionDataContext : IQuestionDataContext
    {
        public async Task<string> GetQuestionsAsync(long currentUnixTime)
        {
            var requestUri = $"https://api.stackexchange.com/2.2/search/advanced?pagesize=100&todate={currentUnixTime}&order=desc&sort=creation&accepted=True&answers=2&site=stackoverflow&filter=!OUZaoY_XieXF)L4(8wA2VoXCcA6i2UanMWOT_Tu5A59";

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
