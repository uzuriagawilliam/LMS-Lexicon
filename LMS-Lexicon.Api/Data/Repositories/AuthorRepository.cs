using LMS.Api.Core.Entities;
using LMS.Api.Core.Repository;
//using LMS.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Api.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LMS_LexiconApiContext db;

        public AuthorRepository(LMS_LexiconApiContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public void Add(Author author)
        {
            db.Add(author);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await db.Author.AnyAsync(g => g.AuthorId == id);
        }
        public bool Any(int? id)
        {
            return db.Author.Any(g => g.AuthorId == id);
        }

        public async Task<Author> FindAsync(int? id)
        {
            return await db.Author.FindAsync(id);
        }

        //public async Task<IEnumerable<Author>> GetAllAuthors()
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await db.Author.ToListAsync();
        }
        public async Task<IEnumerable<Author>> GetAllAuthors(bool includeLiterature)
        {
            return await db.Author.Include(l => l.Literatures).ToListAsync();
        }

        public async Task<Author> GetAuthor(int? id)
        {
            var query = db.Author
                .Include(l => l.Literatures)
                //.ThenInclude(s => s.SubjectId)
                .AsQueryable();

            return await query.FirstOrDefaultAsync(m => m.AuthorId == id);
        }
        public async Task<Author> GetAuthorByName(string name)
        {
            var query = db.Author
                .Include(l => l.Literatures)
                .ThenInclude(s => s.SubjectId)
                .AsQueryable();

            return await query.FirstOrDefaultAsync(m => m.FirstName == name);
        }

        public void Remove(Author author)
        {
            db.Author.Remove(author);
        }

        public void Update(Author author)
        {
            db.Update(author);
        }
    }
}
