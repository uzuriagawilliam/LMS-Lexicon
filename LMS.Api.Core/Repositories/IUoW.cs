using LMS.Api.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Api.Core.Repositories
{
    public interface IUoW
    {
        IAuthorRepository AuthorRepository { get; }
        ILiteratureRepository LiteratureRepository { get; }
        Task CompleteAsync();
    }
}
