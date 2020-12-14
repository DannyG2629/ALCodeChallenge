
namespace ALCodeChallenge.Data.DataEntities
{
    public class Question
	{        
        public int accepted_answer_id { get; set; }

        public int answer_count { get; set; }

        public string body { get; set; }

        public int creation_date { get; set; }

        public bool is_answered { get; set; }

        public string link { get; set; }

        public int question_id { get; set; }

        public string title { get; set; }
    }
}
