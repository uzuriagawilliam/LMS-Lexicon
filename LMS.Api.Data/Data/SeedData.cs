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
//
namespace LMS.Api.Data.Data
{
    public class SeedData
    {
        private static Faker faker;

        public static async Task InitializeAcync(IServiceProvider services)
        {
            var t1 = new DateTime(1919,01,01);
            var t2 = new DateTime(1995,12,31);

            using var db = new LMS_LexiconApiContext(services.GetRequiredService<DbContextOptions<LMS_LexiconApiContext>>());

            if (await db.Author.AnyAsync()) return;

            faker = new Faker("sv");

            //Create Subjects
            var subjects = new List<Subject>();
            for (int i = 0; i < 5; i++)
            {               
                subjects.Add(new Subject
                {
                    Name = faker.Name.JobTitle(),

                });
            }

            
            

            var authors = new List<Author>();
            for (int i = 0; i < 20; i++)
            {
                var faker1 = new Faker("sv");
                int sub = faker.Random.Int(0,4);
                authors.Add(new Author
                {
                    FirstName = faker1.Person.FirstName,
                    LastName = faker1.Person.LastName,
                    BirthDate = faker1.Date.Between(t1,t2),
                    //                    BirthDate = DateTime.Now.AddYears(faker1.Random.Int(-90, -20)),
                    Literatures = GetLiteratures(subjects, sub)

                });
            }
            db.AddRange(authors);
            await db.SaveChangesAsync();

        }
        
// To do: on to many literature => author
        private static ICollection<Literature> GetLiteratures(List<Subject> subjects, int sub)
        {
            var Literatures = new List<Literature>();

            for (int i = 0; i < sub; i++)
            {
                Literatures.Add(new Literature
                {
                    Title = faker.Commerce.ProductName(),
                    PublicationDate = DateTime.Now.AddYears(faker.Random.Int(-100, 0)),
                    Description = faker.Commerce.ProductDescription(),
                    Level = faker.Random.Int(1, 5),
                    Subject = subjects[faker.Random.Int(0, 4)]

                });
            }
            return Literatures;

        }
    }
}

