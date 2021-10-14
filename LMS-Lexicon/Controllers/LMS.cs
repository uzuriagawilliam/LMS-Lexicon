using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Controllers
{
    public class LMS : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
