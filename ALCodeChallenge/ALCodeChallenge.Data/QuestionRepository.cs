using ALCodeChallenge.Data.DataEntities;
using ALCodeChallenge.Data.Interfaces;
using ALCodeChallenge.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALCodeChallenge.Data
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IQuestionDataContext _dataContext;

        public QuestionRepository(IQuestionDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<QuestionDetail>> GetQuestionDetails()
        {            
            var currentUnixTime = DateTimeOffset.Now.ToUnixTimeSeconds();

            var response = await _dataContext.GetQuestions(currentUnixTime);
            var questionResponse = JsonConvert.DeserializeObject<Response<Question>>(response);
            
            return MapToQuestionDetail(questionResponse.items.ToList());
        }

        private IEnumerable<QuestionDetail> MapToQuestionDetail(IEnumerable<Question> questions)
        {
            var questionDetails = new List<QuestionDetail>();

            foreach (Question question in questions)
            {
                questionDetails.Add(new QuestionDetail
                {
                    AcceptedAnswerId = question.accepted_answer_id,
                    AnswerCount = question.answer_count,
                    Body = question.body,
                    CreationDate = DateTimeOffset.FromUnixTimeSeconds(question.creation_date).DateTime,
                    Link = question.link,
                    QuestionId = question.question_id,
                    Title = question.title
                });
            }

            return questionDetails;
        }
    }
}
