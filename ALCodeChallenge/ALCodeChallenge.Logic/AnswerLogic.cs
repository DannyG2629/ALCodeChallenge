using ALCodeChallenge.Data.Interfaces;
using ALCodeChallenge.Logic.Interfaces;
using ALCodeChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALCodeChallenge.Logic
{
    public class AnswerLogic : IAnswerLogic
    {
        // TODO: Additional logic for tracking the number of requests left could be beneficial

        private IAnswerRepository _repository;

        public AnswerLogic(IAnswerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AnswerDetail>> GetAnswerDetailsByQuestionIdAsync(int questionId)
        {
            var answers = await _repository.GetAnswerDetailsByQuestionIdAsync(questionId);
            var returnList = answers.ToList();

            RemoveAnswersWithWrongQuestionId(returnList, questionId);

            if (returnList.Count() <= 1) return new List<AnswerDetail>();                             // There needs to be at least 2 answers
            if (returnList.Count(rl => rl.IsAccepted == true) != 1) return new List<AnswerDetail>();  // There needs to be exactly one accepted answer

            RandomizeAnswerOrder(returnList);

            return returnList;
        }

        private void RemoveAnswersWithWrongQuestionId(List<AnswerDetail> answerDetails, int questionId)
        {
            if (answerDetails.All(qd => qd.QuestionId == questionId)) return;

            var invalidAnswers = answerDetails.Where(qd => qd.QuestionId != questionId).ToList();

            invalidAnswers.ForEach(ia => answerDetails.Remove(ia));
        }

        private void RandomizeAnswerOrder(List<AnswerDetail> answers)
        {
            var rnd = new Random();
            var answerCount = answers.Count();

            for (int i = 0; i < answerCount; i++)
            {
                var randIndex = rnd.Next(answerCount);

                var randomAnswer = answers[randIndex];
                answers[randIndex] = answers[i];
                answers[i] = randomAnswer;
            }
        }
    }
}
