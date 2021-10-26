using LMS_Lexicon.Core.Models.Entities;
using LMS_Lexicon.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_Lexicon.Data.Data
{
    public class LmsDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Course> CourseClass { get; set; }
        public DbSet<Document> DocumentClass { get; set; }
        public DbSet<Module> ModuleClass { get; set; }
        public DbSet<Activity> ActivityClass { get; set; }
        public DbSet<Author> AthorClass { get; set; }//William

        public LmsDbContext(DbContextOptions<LmsDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<Course>().HasKey(c => new { c.UserId, c.Id });
        //}

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<Document>()
        //    .HasOne(a => a.Activity)
        //    .WithMany(m => m.Documents)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.Cascade);
        //}

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<Activity>()
        //    .HasMany(a => a.Documents)
        //    .WithOne(m => m.Activity)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
