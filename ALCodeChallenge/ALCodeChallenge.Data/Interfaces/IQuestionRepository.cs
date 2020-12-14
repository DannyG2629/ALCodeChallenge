using ALCodeChallenge.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALCodeChallenge.Data.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionDetail>> GetQuestionDetails();

    }
}
