using LMS.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Api.Core.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author> GetAuthor(int? id);
        Task<Author> FindAsync(int? id);
        Task<bool> AnyAsync(int? id);
        void Add(Author author);
        void Update(Author author);
        void Remove(Author author);
        public bool Any(int? id);
        Task<Author> GetAuthorByName(string name);
        Task<IEnumerable<Author>> GetAllAuthors(bool includeincludeLiterature);

    }
}
