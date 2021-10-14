using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.Api.Core.Entities;

namespace LMS_Lexicon.Api.Data.Data
{
    public class LMS_LexiconApiContext : DbContext
    {
        public LMS_LexiconApiContext (DbContextOptions<LMS_LexiconApiContext> options)
            : base(options)
        {
        }

        public DbSet<LMS.Api.Core.Entities.Author> Author { get; set; }

        public DbSet<LMS.Api.Core.Entities.Literature> Literature { get; set; }
        public DbSet<LMS.Api.Core.Entities.Subject> Subject { get; set; }
    }
}
