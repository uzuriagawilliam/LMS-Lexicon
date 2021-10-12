using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using LMS_Lexicon.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bogus;

namespace LMS_Lexicon.Data
{
    public class SeedData
    {
        private static Faker fake;
        private static LmsDbContext db;
        private static RoleManager<IdentityRole> roleManager;
        private static UserManager<ApplicationUser> userManager;



        internal static async Task InitAsync(LmsDbContext context, IServiceProvider services) 
        {
            //if (string.IsNullOrWhiteSpace(adminPW)) throw new Exception("Cant get password from config");
            if (context is null) throw new NullReferenceException(nameof(LmsDbContext));

            db = context;

            if (db.CourseClass.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (roleManager is null) throw new NullReferenceException(nameof(RoleManager<IdentityRole>));

            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            if (userManager is null) throw new NullReferenceException(nameof(UserManager<ApplicationUser>));

            var roleNames = new[] { "Students", "Teacher" };
            var adminEmail = "lms@lms.se";

            using (var db = services.GetRequiredService<LmsDbContext>())
            {

                if (await db.Users.AnyAsync()) return;

                const string passWord = "bytmig";
                const string roleName = "Student";

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

                var role = new IdentityRole<int> { Name = roleName };
                var addRoleResult = await roleManager.CreateAsync(role);

                fake = new  Faker("sv");

                var students = GetUsers(100);
                //var modules = GetModules();
                //await db.AddRangeAsync(students);

                //foreach(var student in students)
                //{
                //    var result = await userManager.CreateAsync(student, passWord);
                //    if (!result.Succeeded) throw new Exception(String.Join("\n", result.Errors));
                //    await userManager.AddToRoleAsync(student, roleName);
                //}
                var courses = GetCourses();
                await db.AddRangeAsync(courses);

                //var enrollments = GetEnrollments(students, courses);
                //await db.AddRangeAsync(enrollments);

                await db.SaveChangesAsync();  

            }

        }

        private static List<Course> GetCourses() 
        {
            var courses = new List<Course>();

            for (int i =0; i < 20; i++)
            {
                var course = new Course
                {
                    CourseName = fake.Commerce.ProductName(),
                    Description = fake.Lorem.Sentence(),
                    StartDate = System.DateTime.Now.AddDays(fake.Random.Int(-5,5))    //fake.Date.Between(2021 - 10 - 11, 2021 - 10 - 30)

                };
                courses.Add(course);

            }

            return courses;
        }

        private static object GetUsers(int count)
        {
            return null;
        }

        //private static List<Module> GetModules(List<Document> documents, List<Activity> activities)
        //{
        //    var modules = new List<Module>();

        //    foreach(var document in documents)
        //    {
        //        foreach(var activity in activities)
        //        {
        //            var module = new Module
        //            {
        //                Documents = document,
        //                Activities = activity,
        //                 StartDate =  fake
        //                };
        //                modules.Add(enrollment);
        //            }
        //        }
        //        return modules;
        //    }
        //}
    }
}
