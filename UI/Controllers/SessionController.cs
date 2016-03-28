using Microsoft.AspNet.Mvc;

namespace UI.Controllers
{
    public class SessionController : Controller
    {
        [HttpGet, Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login(string username, string password)
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
