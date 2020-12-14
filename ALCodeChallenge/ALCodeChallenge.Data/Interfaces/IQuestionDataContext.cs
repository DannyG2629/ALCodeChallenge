using System.Threading.Tasks;


namespace ALCodeChallenge.Data.Interfaces
{
    public interface IQuestionDataContext
    {
        Task<string> GetQuestions(long currentUnixTime);
    }
}
