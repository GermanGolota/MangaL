using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaLWebAPI.Controllers
{
    public class MangasController : Controller
    {
        public IActionResult Index()
        {
            //show all mangas with searchbox
            return View();
        }
        public IActionResult View([FromRoute] string id)
        {
            //show manga overview page with selected id
            return View();
        }
        public IActionResult Chapter([FromRoute] string chapter)
        {
            //show specified chapter of specified manga(with images)
            return View();
        }
    }
}
