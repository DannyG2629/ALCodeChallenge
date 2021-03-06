﻿using ALCodeChallenge.Data.DataEntities;
using ALCodeChallenge.Data.Interfaces;
using ALCodeChallenge.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ALCodeChallenge.Data
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IAnswerDataContext _dataContext;

        public AnswerRepository(IAnswerDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<AnswerDetail>> GetAnswerDetailsByQuestionIdAsync(int questionId)
        {                        
            var response = await _dataContext.GetAnswersAsync(questionId);

            try
            {
                var answerResponse = JsonConvert.DeserializeObject<Response<Answer>>(response);

                return MapToAnswerDetail(answerResponse.items.ToList());
            }
            catch (Exception)
            {
                return new List<AnswerDetail>();  // TODO: Return or log a meaningful error message on failure
            }            
        }

        private IEnumerable<AnswerDetail> MapToAnswerDetail(IEnumerable<Answer> answers)
        {
            var answerDetails = new List<AnswerDetail>();

            foreach (Answer answer in answers)
            {                
                answerDetails.Add(new AnswerDetail
                {                    
                    AnswerId = answer.answer_id,
                    Body = answer.body,
                    IsAccepted = answer.is_accepted,
                    Link = answer.link,
                    QuestionId = answer.question_id
                });
            }

            return answerDetails;
        }
    }
}
