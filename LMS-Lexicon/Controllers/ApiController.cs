using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS_Lexicon.Data;
using System.Net.Http;
using LMS_Lexicon.Models;
using Newtonsoft.Json;
using System.Text.Json;
//using LMS.Api.Core.Dtos;
//using LMS_Api.Core.Dtos;
//using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace LMS_Lexicon.Controllers
{
    public class ApiController : Controller
    {
        private HttpClient httpClient;

        public ApiController()
        {
            httpClient = new HttpClient(new HttpClientHandler());
            //{ AutomaticDecompression = System.Net.DecompressionMethods.GZip });

            httpClient.BaseAddress = new Uri("https://localhost:44390");
        }

        // GET: Authors
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var res = await SimpleGet();
            //var res2 = await SimpleGetLiterature();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (!String.IsNullOrEmpty(searchString))
            {
                res = res.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    res = res.OrderByDescending(s => s.Name);
                    break;
                default:
                    res = res.OrderBy(s => s.Name);
                    break;
            }

            return View(res);
        }
        public async Task<IActionResult> LiteratureIndex(string sortOrder)
        {
            var res = await SimpleGetLiterature();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            switch (sortOrder)
            {
                case "name_desc":
                    res = res.OrderByDescending(s => s.Title);
                    break;
                default:
                    res = res.OrderBy(s => s.Title);
                    break;
            }

            return View(res);
        }

        private async Task<IEnumerable<AuthorsDto>> SimpleGet()
        {
            var response = await httpClient.GetAsync("api/Authors");
            //IEnumerable<AuthorsDto> authorsDto;
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var codeEvents = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<AuthorsDto>>(content, new
                JsonSerializerOptions
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return codeEvents;
        }
        private async Task<IEnumerable<AuthorsDto>> SimpleGetId(int Id)
        {
            var response = await httpClient.GetAsync($"api/Authors/{Id}");
            //IEnumerable<AuthorsDto> authorsDto;
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var codeEvents = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<AuthorsDto>>(content, new
                JsonSerializerOptions
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });


            return codeEvents;
        }
        private async Task<IEnumerable<Models.LiteratureDto>> SimpleGetLiterature()
        {
            var response = await httpClient.GetAsync("api/Literatures");
            //IEnumerable<AuthorsDto> authorsDto;
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var codeEvents = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Models.LiteratureDto>>(content, new
                JsonSerializerOptions
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return codeEvents;
        }
    }
}
