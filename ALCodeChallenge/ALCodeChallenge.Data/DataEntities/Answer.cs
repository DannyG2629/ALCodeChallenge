
namespace ALCodeChallenge.Data.DataEntities
{
    public class Answer
    {
        public int answer_id { get; set; }

        public string body { get; set; }

        public bool is_accepted { get; set; }

        public string link { get; set; }

        public int question_id { get; set; }	
    }
}
