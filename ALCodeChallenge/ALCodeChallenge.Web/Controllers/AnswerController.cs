using ALCodeChallenge.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace ALCodeChallenge.Web.Controllers
{
    [Route("Answer")]
    public class AnswerController : Controller
    {
        private readonly IAnswerLogic _answerLogic;

        public AnswerController(IAnswerLogic answerLogic)
        {
            _answerLogic = answerLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetAnswerDetailsByQuestionId")]  // Answers could be cached if logic for randomizing order was moved client side        
        public async Task<JsonResult> GetAnswerDetailsByQuestionIdAsync(int questionId)
        {
            var answers = await _answerLogic.GetAnswerDetailsByQuestionIdAsync(questionId);

            return Json(answers);
        }
    }
}
