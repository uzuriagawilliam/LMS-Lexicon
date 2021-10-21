using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS.Api.Core.Entities;
using LMS_Lexicon.Api.Data.Data;
using LMS.Api.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace LMS_Lexicon.Api.Controllers
{
 //      [Route("api/authors/{authorId}/litteratures")]
      [Route("api/[controller]")]
    [ApiController]
    public class LiteraturesController : ControllerBase
    {
        private readonly LMS_LexiconApiContext _context;
        private readonly IUoW uow;
        private readonly IMapper mapper;

        public LiteraturesController(LMS_LexiconApiContext context, IUoW uow)
        {
            _context = context;
            this.uow = uow;
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Literature>>> GetLiteraturesForAuthor(int AuthorID)
        //{
        //     var literature = await uow.LiteratureRepository.GetAllLiteratures();
        //            return Ok(literature);
        //}
       
        // GET: api/Literatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Literature>>> GetLiteratures()
        {
            var literature = await uow.LiteratureRepository.GetAllLiteratures();
            return Ok(literature);
        }

        // GET: api/Literatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LiteratureDto>> GetLiterature(int id)
        {
            var literature = await uow.LiteratureRepository.FindAsync(id);

            if (literature == null)
            {
                return NotFound();
            }

            return Ok(literature);
        }

        // PUT: api/Literatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLiterature(int id, Literature literature)
        {
            if (id != literature.Id)
            {
                return BadRequest();
            }

            _context.Entry(literature).State = EntityState.Modified;

            try
            {
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LiteratureExists(id))
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

        // POST: api/Literatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Literature>> PostLiterature(Literature literature)
        {
            uow.LiteratureRepository.Add(literature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLiterature", new { id = literature.Id }, literature);
        }

        // DELETE: api/Literatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLiterature(int id)
        {
            var literature = await uow.LiteratureRepository.FindAsync(id);
            if (literature == null)
            {
                return NotFound();
            }

            uow.LiteratureRepository.Remove(literature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LiteratureExists(int id)
        {
            return uow.LiteratureRepository.Any(id);
        }
    }
}
