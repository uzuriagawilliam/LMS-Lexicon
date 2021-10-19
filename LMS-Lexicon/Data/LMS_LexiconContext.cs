using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.Api.Core.Entities;
using LMS_Lexicon.Api.Core.Dtos;

namespace LMS_Lexicon.Data
{
    public class LMS_LexiconContext : DbContext
    {
        public LMS_LexiconContext (DbContextOptions<LMS_LexiconContext> options)
            : base(options)
        {
        }

        public DbSet<LMS.Api.Core.Entities.Author> Author { get; set; }

        public DbSet<LMS_Lexicon.Api.Core.Dtos.AuthorsDto> AuthorsDto { get; set; }
    }
}
