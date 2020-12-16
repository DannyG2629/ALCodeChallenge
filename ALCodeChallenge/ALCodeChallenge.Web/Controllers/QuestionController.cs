using ALCodeChallenge.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace ALCodeChallenge.Web.Controllers
{
    [Route("Question")]
    public class QuestionController : Controller
    {        
        private readonly IQuestionLogic _questionLogic;

        public QuestionController(IQuestionLogic questionLogic)
        {            
            _questionLogic = questionLogic;
        }

        public async Task<IActionResult> Index()
        {            
            return View();
        }

        [HttpGet("GetQuestionDetails")]
        [ResponseCache(Duration = 900, Location = ResponseCacheLocation.Any, NoStore = false)]  // Questions are cached for 15 minutes
        public async Task<JsonResult> GetQuestionDetailsAsync()
        {
            var questions = await _questionLogic.GetQuestionDetailsAsync();

            return Json(questions);
        }
    }
}
