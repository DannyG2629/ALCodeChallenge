using System.Threading.Tasks;


namespace ALCodeChallenge.Data.Interfaces
{
    public interface IAnswerDataContext
    {
        Task<string> GetAnswersAsync(int questionId);
    }
}
