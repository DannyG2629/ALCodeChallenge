using ALCodeChallenge.Logic.Interfaces;
using ALCodeChallenge.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ALCodeChallenge.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IQuestionLogic _questionLogic;

        public QuestionController(ILogger<QuestionController> logger, IQuestionLogic questionLogic)
        {
            _logger = logger;
            _questionLogic = questionLogic;
        }

        public async Task<IActionResult> Index()
        {            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<JsonResult> GetQuestionDetails()
        {
            var questions = await _questionLogic.GetQuestionDetails();

            return Json(questions);
        }
    }
}
