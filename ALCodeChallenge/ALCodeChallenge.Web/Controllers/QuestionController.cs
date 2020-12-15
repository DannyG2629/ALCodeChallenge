using ALCodeChallenge.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace ALCodeChallenge.Web.Controllers
{
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

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<JsonResult> GetQuestionDetailsAsync()
        {
            var questions = await _questionLogic.GetQuestionDetailsAsync();

            return Json(questions);
        }
    }
}
