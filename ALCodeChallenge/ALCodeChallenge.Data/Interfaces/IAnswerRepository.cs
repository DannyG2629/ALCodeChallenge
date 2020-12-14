using ALCodeChallenge.Model;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ALCodeChallenge.Data.Interfaces
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<AnswerDetail>> GetAnswerDetailsByQuestionId(int questionId);
    }
}
