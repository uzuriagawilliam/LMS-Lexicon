using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Api.Core.Entities;
using LMS_Lexicon.Api.Data.Data;
using LMS.Api.Core.Repositories;
using LMS_Lexicon.Api.Dtos;
using AutoMapper;
using System.Collections;
using LMS_Lexicon.Api.Core.Dtos;

namespace LMS_Lexicon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LMS_LexiconApiContext _context;
        private readonly IUoW uow;
        private readonly IMapper mapper;

        public AuthorsController(IUoW uow, LMS_LexiconApiContext context, IMapper mapper)
        {
             this._context = context;
             this.uow = uow;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorsDto>>> GetAllAuthors(bool includeLiterature)
        
        {
            //Return AuthorsDto
            if (includeLiterature)
            {
                var author = await uow.AuthorRepository.GetAllAuthors(includeLiterature);

                var dto = mapper.Map<IEnumerable<AuthorsDto>>(author); 

                return Ok(dto);
            }
            else
            {
                var author = await uow.AuthorRepository.GetAllAuthors();                
 
                var dto = mapper.Map<IEnumerable<AuthorsDto>>(author);

                return Ok(dto);                
            }           
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorsDto>> GetAuthor(int id)
        {            
            //var author = await uow.AuthorRepository.FindAsync(id);
            var author = await uow.AuthorRepository.GetAuthor(id);
            var NewAuthor = new AuthorsDto();
            NewAuthor.Name = $"{author.FirstName} {author.LastName}";
            DateTime Now = DateTime.Now;
            NewAuthor.Age = Now.Year - author.BirthDate.Year;
            NewAuthor.Literatures = author.Literatures;

            if (NewAuthor == null)
            {
                return NotFound();
            }

            return Ok(NewAuthor);
        }
       
        /*[HttpGet("{name}")]
        public async Task<ActionResult<Author>> GetAuthor(string name)
        {
            //var author = await uow.AuthorRepository.FindAsync(id);
            var author = await uow.AuthorRepository.GetAuthorByName(name);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }*/

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }
                        
            _context.Entry(author).State = EntityState.Modified;//???

            try
            {
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            uow.AuthorRepository.Add(author);
            await uow.CompleteAsync();

            return CreatedAtAction("GetAuthor", new { id = author.AuthorId }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await uow.AuthorRepository.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            uow.AuthorRepository.Remove(author);
            await uow.CompleteAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return uow.AuthorRepository.Any(id);
        }
    }
}
