using ALCodeChallenge.Data.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace ALCodeChallenge.Data.DataEntities
{
    public class AnswerDataContext : IAnswerDataContext
    {
        public async Task<string> GetAnswers(int questionId)
        {
            var requestUri = $"https://api.stackexchange.com/2.2/questions/{questionId}/answers?pagesize=100&order=desc&sort=activity&site=stackoverflow&filter=!)sBhRh8NkgpEppLV((Hu";

            try
            {
                using (var httpClient = new HttpClient())
                using (var response = await httpClient.GetAsync(requestUri))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
