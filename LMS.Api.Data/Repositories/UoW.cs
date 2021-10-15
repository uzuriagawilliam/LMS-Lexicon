using LMS.Api.Core.Repositories;
using LMS.Api.Core.Repository;
using LMS_Lexicon.Api.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Api.Data.Repositories
{
    public class UoW : IUoW
    {
        private readonly LMS_LexiconApiContext db;
        public IAuthorRepository AuthorRepository { get; }
        public ILiteratureRepository LiteratureRepository { get; }

        public UoW(LMS_LexiconApiContext db)
        {
            this.db = db;
            AuthorRepository = new AuthorRepository(db);
            LiteratureRepository = new LiteratureRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }

    }
}
