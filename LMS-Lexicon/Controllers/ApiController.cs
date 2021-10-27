using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS_Lexicon.Data;
using System.Net.Http;
using LMS_Lexicon.Core.Models;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
//using LMS.Api.Core.Dtos;
//using LMS_Api.Core.Dtos;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.AspNetCore.JsonPatch;

namespace LMS_Lexicon.Controllers
{
    public class ApiController : Controller
    {
        private HttpClient httpClient;
        private const string json = "application/json";
        private readonly IHttpClientFactory httpClientFactory;
        //private readonly CodeEventClient codeEventClient;

        public ApiController()
        {
            httpClient = new HttpClient(new HttpClientHandler(){ AutomaticDecompression = System.Net.DecompressionMethods.GZip });

            httpClient.BaseAddress = new Uri("https://localhost:44390");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

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
        private async Task<IEnumerable<Author>> SimpleGetId(int Id)
        {
            var response = await httpClient.GetAsync($"api/Authors/{Id}");
            //IEnumerable<AuthorsDto> authorsDto;
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var codeEvents = JsonSerializer.Deserialize<Author>(content, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase });


            return null;// codeEvents;
        }
        private async Task<IEnumerable<LiteratureDto>> SimpleGetLiterature()
        {
            var response = await httpClient.GetAsync("api/Literatures");
            //IEnumerable<AuthorsDto> authorsDto;
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var codeEvents = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<LiteratureDto>>(content, new
                JsonSerializerOptions
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return codeEvents;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create(Author author)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Authors");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));            
            

            var serializedAuthor = JsonSerializer.Serialize(author);

            request.Content = new StringContent(serializedAuthor);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(json);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var codeEvents = JsonSerializer.Deserialize<Author>(content, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            // return codeEvents;
            // return null;

            return RedirectToAction("Index", "Api");
 
        }

        public async Task <IActionResult> Edit(int id)
        {
            var response = await httpClient.GetAsync($"api/Authors/{id}");
            //IEnumerable<AuthorsDto> authorsDto;
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var codeEvents = JsonSerializer.Deserialize<Author>(content, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

           return View(codeEvents);                       
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(Author author)
        {

            var patchDocument = new JsonPatchDocument<Author>();

            //Vad göra
            //patchDocument.Remove(a => a.BirthDate);

            //patchDocument.Replace(a => a.FirstName, author.FirstName);
            //patchDocument.Replace(a => a.LastName, author.LastName);
            //patchDocument.Replace(a => a.BirthDate, author.BirthDate);



            var serializedPatchDocument = JsonConvert.SerializeObject(patchDocument);

            var request = new HttpRequestMessage(HttpMethod.Patch, "api/author");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));
            request.Content = new StringContent(serializedPatchDocument);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("appllication/json-patch+json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {            
            var response = await httpClient.GetAsync($"api/Authors/{id}");
           
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var codeEvents = JsonSerializer.Deserialize<Author>(content, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });


           return View(codeEvents);           
        }



        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response =  await httpClient.DeleteAsync($"api/Authors/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return RedirectToAction("Index", "Api");
        }

    }
}
