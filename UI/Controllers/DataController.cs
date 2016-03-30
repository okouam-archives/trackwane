using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace UI.Controllers
{
    public class DataController : Controller
    {
        [HttpGet, Route("data")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, Route("data/historical")]
        public IActionResult Historical()
        {
            return View();
        }
    }
}
