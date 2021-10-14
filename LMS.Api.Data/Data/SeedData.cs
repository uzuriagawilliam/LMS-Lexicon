using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_Lexicon.Api.Data.Data;
using LMS.Api.Data.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LMS.Api.Core.Entities;

namespace LMS.Api.Data.Data
{
    public class SeedData
    {
        public static async Task InitializeAcync(IServiceProvider services)
        {

            using var db = new LMS_LexiconApiContext(services.GetRequiredService<DbContextOptions<LMS_LexiconApiContext>>());

            if (await db.Author.AnyAsync()) return;

            var faker = new Faker("sv");
            
            var Authors = new List<Author>();
            var Literatures = new List<Literature>();
            var Subjects = new List<Subject>();

            for (int i = 0; i < 20; i++)
            {
                Authors.Add(new Author
                {

                    FirstName = faker.Person.FirstName,
                    LastName = faker.Person.LastName,
                    BirthDate = DateTime.Now.AddDays(faker.Random.Int(-60, 0)),
                });
            }
            db.AddRange(Authors);
            await db.SaveChangesAsync();

            for (int i = 0; i < 20; i++)
            {
                Literatures.Add(new Literature
                {
                    Title = faker.Commerce.ProductName(),
                    PublicationDate = DateTime.Now.AddDays(faker.Random.Int(-60, 0)),
                    Description = faker.Commerce.ProductDescription(),
                    Level = faker.Random.Int(1,5),
 //                   Id = Authors[i].AuthorId,
//                    CourseId = courses[i].Id Testa att inte går sönder, lägg till id i kopplingstabellen
                });
            }


            db.AddRange(Literatures);
            await db.SaveChangesAsync();




            for (int i = 0; i < 20; i++)
            {
                int j = faker.Random.Int(0, 19);
                Subjects.Add(new Subject
                {
                    Name = faker.Name.JobTitle(),

                    Literatures = (ICollection<Literature>)Literatures[j]
                    //                    CourseId = courses[i].Id
                });
            }


            db.AddRange(Subjects);
            await db.SaveChangesAsync();



            for (int i = 0; i < 20; i++)
            {
                // skapa nya rader i AuthorLiterature-tabellen
                // använd author och litarature id
            }
        }
    }
}

