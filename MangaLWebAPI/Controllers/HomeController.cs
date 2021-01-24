using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MangaLWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //show welcome page
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
