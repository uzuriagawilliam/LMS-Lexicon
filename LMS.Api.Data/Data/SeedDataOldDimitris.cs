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

        //       var subjects = new List<Subject>();

        public static async Task InitializeAcync(IServiceProvider services)
        {

            using var db = new LMS_LexiconApiContext(services.GetRequiredService<DbContextOptions<LMS_LexiconApiContext>>());

            if (await db.Author.AnyAsync()) return;

            faker = new Faker("sv");

            //Create Subjects
            var subjects = new List<Subject>();
            for (int i = 0; i < 5; i++)
            {
                //                int j = faker.Random.Int(0, 19);
                subjects.Add(new Subject
                {
                    Name = faker.Name.JobTitle(),

                    //                   Literatures = (ICollection<Literature>)Literatures[j]
                    //                    CourseId = courses[i].Id
                });
            }


            var authors = new List<Author>();
            for (int i = 0; i < 20; i++)
            {
                authors.Add(new Author
                {
                    FirstName = faker.Person.FirstName,
                    LastName = faker.Person.LastName,
                    BirthDate = DateTime.Now.AddYears(faker.Random.Int(-90, -20)),
                    Literatures = GetLiteratures(subjects)

                });
            }
            db.AddRange(authors);
            await db.SaveChangesAsync();
 //           db.Add(authors);
 //           db.SaveChanges();
            

            //var Authors = new List<Author>();
            //var Literatures = new List<Literature>();
            //var Subjects = new List<Subject>();

            //for (int i = 0; i < 20; i++)
            //{
            //    Literatures.Add(new Literature
            //    {
            //        Title = faker.Commerce.ProductName(),
            //        PublicationDate = DateTime.Now.AddYears(faker.Random.Int(-100, 0)),
            //        Description = faker.Commerce.ProductDescription(),
            //        Level = faker.Random.Int(1, 5),
            //    });
            //}
            //var AuthorsLiteratures = new List<AuthorLiterature>();

            //for (int i = 0; i < 20; i++)
            //{
            //    var f = new Faker("sv");
            //    Authors.Add(new Author
            //    {
            //        FirstName = f.Person.FirstName,
            //        LastName = f.Person.LastName,
            //        BirthDate = DateTime.Now.AddYears(faker.Random.Int(-90, -20)),
            //    });
            //}
            //db.AddRange(Authors);
            //await db.SaveChangesAsync();




            //           db.AddRange(Literatures);
            //           await db.SaveChangesAsync();

            //           int Ind = faker.Random.Int(0, 5);
            //           int Ynd = faker.Random.Int(0, 5);

            //          literature = new < Literature >[];
            //          Literatures.CopyTo(litearray);
            /*
                        var literatureSubject = new List<Literature>();
                        foreach ( var j in Literatures) {
                            literatureSubject.Add(Literatures[j]);
                        }

                        for (int k=0; k<5; k++)
                        { 
                            Subjects.Add(new Subject)
                        }
            */
            //           Subjects.Add(new Subject
            //           {
            //              Name = faker.Commerce.Product(),
            //                Literatures = Literatures[Ind]

            /*               
                                           Name = faker.Commerce.Product(),
                                           Literatures1 = new Literature[]
                                           {
                                               new Literature
                                               {
                                                   Title = Literatures[Ind].Title,
                                                   PublicationDate = Literatures[Ind].PublicationDate,
                                                   Description = Literatures[Ind].Description,
                                                   Level = Literatures[Ind].Level,
                                               },
                                               new Literature
                                               {
                                                   Title = Literatures[Ynd].Title,
                                                   PublicationDate = Literatures[Ynd].PublicationDate,
                                                   Description = Literatures[Ynd].Description,
                                                   Level = Literatures[Ynd].Level,
                                               }
                                           },

                       });
                       db.AddRange(Subjects);
                       await db.SaveChangesAsync();




           */

            //  for (int i = 0; i < 20; i++)
            //{
            // skapa nya rader i AuthorLiterature-tabellen
            // använd author och litarature id
            //  int j = faker.Random.Int(0, 9);
            //

            //for (int i = 0; i < 20; i++)
            //{
            //    // skapa nya rader i AuthorLiterature-tabellen
            //    // använd author och litarature id
            //    int j = faker.Random.Int(0, 19);
            //    int l = faker.Random.Int(0, 19);
            //    AuthorsLiteratures.Add(new AuthorLiterature
            //    {
            //        AuthorId = Authors[j].AuthorId,
            //        LiteratureId = Literatures[l].LiteratureId,
            //    });
            //}
            //db.AddRange(AuthorsLiteratures);//Do not work
            //   await db.SaveChangesAsync();

        }
        

        private static ICollection<Literature> GetLiteratures(List<Subject> subjects)
        {
            var Literatures = new List<Literature>();

            for (int i = 0; i < 20; i++)
            {
                Literatures.Add(new Literature
                {
                    Title = faker.Commerce.ProductName(),
                    PublicationDate = DateTime.Now.AddYears(faker.Random.Int(-100, 0)),
                    Description = faker.Commerce.ProductDescription(),
                    Level = faker.Random.Int(1, 5),
                    Subject = subjects[faker.Random.Int(0, 4)]
 //                   Subject = new Subject() { Name = "Test"}
                });
            }
            return Literatures;

        }
    }
}

