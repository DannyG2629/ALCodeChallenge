using ALCodeChallenge.Data.Interfaces;
using ALCodeChallenge.Logic.Interfaces;
using ALCodeChallenge.Model;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<QuestionDetail>> GetQuestionDetailsAsync()
        {
            var questionDetails = await _repository.GetQuestionDetailsAsync();
            var returnList = questionDetails.ToList();

            RemoveQuestionsWithoutAcceptedAnswer(returnList);
            RemoveQuestionsWithoutMultipleAnswers(returnList);

            return returnList;
        }

        private void RemoveQuestionsWithoutAcceptedAnswer(List<QuestionDetail> questionDetails)
        {
            if (questionDetails.All(qd => qd.AcceptedAnswerId != null)) return;

            var unacceptedQuestions = questionDetails.Where(qd => qd.AcceptedAnswerId == null).ToList();

            unacceptedQuestions.ForEach(uq => questionDetails.Remove(uq));
        }

        private void RemoveQuestionsWithoutMultipleAnswers(List<QuestionDetail> questionDetails)
        {
            if (questionDetails.All(qd => qd.AnswerCount > 1)) return;

            var invalidCountQuestions = questionDetails.Where(qd => qd.AnswerCount <= 1).ToList();

            invalidCountQuestions.ForEach(iq => questionDetails.Remove(iq));
        }
    }
}
