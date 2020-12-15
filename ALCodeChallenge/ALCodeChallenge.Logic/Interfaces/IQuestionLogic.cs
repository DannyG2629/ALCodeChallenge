using ALCodeChallenge.Model;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ALCodeChallenge.Logic.Interfaces
{
    public interface IQuestionLogic
    {
        Task<IEnumerable<QuestionDetail>> GetQuestionDetailsAsync();
    }
}
