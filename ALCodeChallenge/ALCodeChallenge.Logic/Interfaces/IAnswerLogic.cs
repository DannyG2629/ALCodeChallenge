using ALCodeChallenge.Model;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ALCodeChallenge.Logic.Interfaces
{
    public interface IAnswerLogic
    {
        Task<IEnumerable<AnswerDetail>> GetAnswerDetailsByQuestionId(int questionId);
    }
}
