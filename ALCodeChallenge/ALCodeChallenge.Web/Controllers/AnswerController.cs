using ALCodeChallenge.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ALCodeChallenge.Web.Controllers
{
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

        public JsonResult GetAnswerDetailsByQuestionId(int questionId)
        {
            var answers = _answerLogic.GetAnswerDetailsByQuestionId(questionId);

            return Json(answers);
        }
    }
}
