using System;

namespace ALCodeChallenge.Model
{
    public class QuestionDetail
    {
        public int? AcceptedAnswerId { get; set; }

        public int AnswerCount { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public string Link { get; set; }

        public int QuestionId { get; set; }

        public string Title { get; set; }
    }
    
}
