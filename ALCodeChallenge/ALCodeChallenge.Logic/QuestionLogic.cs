using ALCodeChallenge.Data.Interfaces;
using ALCodeChallenge.Logic.Interfaces;
using ALCodeChallenge.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALCodeChallenge.Logic
{
    public class QuestionLogic : IQuestionLogic
    {
        private IQuestionRepository _repository;

        public QuestionLogic(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<QuestionDetail>> GetQuestionDetails()
        {
            return await _repository.GetQuestionDetails();
        }
    }
}
