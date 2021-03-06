using LMS.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Api.Core.Repositories
{
    public interface ILiteratureRepository
    {
        Task<IEnumerable<Literature>> GetAllLiteratures();
        Task<Literature> GetLiterature(int? id);
        Task<Literature> FindAsync(int? id);
        Task<bool> AnyAsync(int? id);
        void Add(Literature literature);
        void Update(Literature literature);
        void Remove(Literature literature);
        public bool Any(int? id);
    }
}
