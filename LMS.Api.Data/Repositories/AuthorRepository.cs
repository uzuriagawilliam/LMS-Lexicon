using LMS.Api.Core.Entities;
using LMS.Api.Core.Repository;
using LMS_Lexicon.Api.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Api.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LMS_LexiconApiContext db;

        public AuthorRepository (LMS_LexiconApiContext db)
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

        public async Task<Author> FindAsync(int? id)
        {
            return await db.Author.FindAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await db.Author.ToListAsync();
        }

        public async Task<Author> GetAuthor(int? id)
        {
            return await db.Author
                .FirstOrDefaultAsync(m => m.AuthorId == id);
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
