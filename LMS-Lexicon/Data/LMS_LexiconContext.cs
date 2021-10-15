using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS_Lexicon.Models.Entities;

namespace LMS_Lexicon.Data
{
    public class LMS_LexiconContext : DbContext
    {
        public LMS_LexiconContext (DbContextOptions<LMS_LexiconContext> options)
            : base(options)
        {
        }

        public DbSet<LMS_Lexicon.Models.Entities.Course> Course { get; set; }

        public DbSet<LMS_Lexicon.Models.Entities.Module> Module { get; set; }
    }
}
