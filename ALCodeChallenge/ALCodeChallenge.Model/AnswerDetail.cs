using System;
using System.Collections.Generic;
using System.Text;

namespace ALCodeChallenge.Model
{
    public class AnswerDetail
    {
        public int AnswerId { get; set; }

        public string Body { get; set; }

        public bool IsAccepted { get; set; }

        public string Link { get; set; }

        public int QuestionId { get; set; }
    }
}
