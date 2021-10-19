using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Api.Core.Entities;
using LMS_Lexicon.Data;
using System.Net.Http;
using LMS_Lexicon.Models;
using Newtonsoft.Json;
using System.Text.Json;
//using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace LMS_Lexicon.Controllers
{
    public class AuthorsController : Controller
    {
        public AuthorsController()
        {

        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {

            return View();
        }    
    }
}
