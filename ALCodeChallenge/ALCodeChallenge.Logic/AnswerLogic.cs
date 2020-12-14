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
        private IAnswerRepository _repository;

        public AnswerLogic(IAnswerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AnswerDetail>> GetAnswerDetailsByQuestionId(int questionId)
        {
            var answers = await _repository.GetAnswerDetailsByQuestionId(questionId);

            RandomizeAnswerOrder(answers.ToList());

            return answers;
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
