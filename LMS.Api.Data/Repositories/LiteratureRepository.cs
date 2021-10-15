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
    public class LiteratureRepository : ILiteratureRepository
    {
        private readonly LMS_LexiconApiContext db;

        public LiteratureRepository(LMS_LexiconApiContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public void Add(Literature literature)
        {
            db.Add(literature);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await db.Literature.AnyAsync(g => g.Id == id);
        }
        public bool Any(int? id)
        {
            return db.Literature.Any(g => g.Id == id);
        }

        public async Task<Literature> FindAsync(int? id)
        {
            return await db.Literature.FindAsync(id);
        }

        public async Task<IEnumerable<Literature>> GetAllLiteratures()
        {
            return await db.Literature.ToListAsync();
        }

        public async Task<Literature> GetLiterature(int? id)
        {
            return await db.Literature
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Remove(Literature literature)
        {
            db.Literature.Remove(literature);
        }

        public void Update(Literature literature)
        {
            db.Update(literature);
        }

    }
}
