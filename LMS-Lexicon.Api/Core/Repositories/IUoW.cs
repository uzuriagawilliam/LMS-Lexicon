using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Lexicon.Api.Core.Repositories
{
    public interface IUoW
    {
        IAuthorRepository AuthorRepository { get; }
        ILiteratureRepository LiteratureRepository { get; }
        Task CompleteAsync();
    }
}
