using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LMS_Lexicon.Core.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Bogus;

namespace LMS_Lexicon.Data.Data
{
    public class SeedData
    {
        private static Faker fake;
        private static LmsDbContext db;
        private static RoleManager<IdentityRole> roleManager;
        private static UserManager<ApplicationUser> userManager;

        public static async Task InitAsync(LmsDbContext context, IServiceProvider services,string userPW) 
        {
            if (string.IsNullOrWhiteSpace(userPW)) throw new Exception("Cant get password from config");
            if (context is null) throw new NullReferenceException(nameof(LmsDbContext));
            db = context;
            fake = new Faker("sv");
            //db.Database.EnsureDeleted();
            //db.Database.Migrate();
       

            if (await db.Users.AnyAsync()) return;

                userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                if (userManager is null) throw new NullReferenceException(nameof(UserManager<ApplicationUser>));
                roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                if (roleManager is null) throw new NullReferenceException(nameof(RoleManager<IdentityRole>));

                var roleNames = new[] { "Student", "Teacher" };
                var userEmail = "test2@lms.se";

                await AddRolesAsync(roleNames);

                const string roleName = "Teacher";
                const string roleStudent = "Student";

                var role = new IdentityRole { Name = roleName };
                var addRoleResult = await roleManager.CreateAsync(role);
                var user = await AddUserAsync(userEmail, userPW);
                await AddToRolesAsync(user, roleName);
            
                var students = GetStudents();

                foreach (var student in students)
                {
                    var result = await userManager.CreateAsync(student, userPW);
                    if (!result.Succeeded) throw new Exception(String.Join("\n", result.Errors));
                    await userManager.AddToRoleAsync(student, roleStudent);
                }

                await CreateActivityType(db);
                var courses = GetCourses();
                await db.AddRangeAsync(courses);
                await db.SaveChangesAsync();
        }

        private static async Task CreateActivityType(LmsDbContext db)
        {
            var activitytypes = new List<ActivityType>();
            var fake = new Faker("sv");

            for (int i = 0; i < 5; i++)
            {
                string name = fake.Commerce.ProductName();
                name = name.Length < 25 ? name : name.Substring(0, 25);

                var activityType = new ActivityType
                {
                    Name = name
                };

                activitytypes.Add(activityType);
               
            }
            await db.AddRangeAsync(activitytypes);
            await db.SaveChangesAsync();
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task AddToRolesAsync(ApplicationUser user, string roleName)
        {
            if (user is null) throw new NullReferenceException(nameof(user));

                var result = await userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
        }
        private static async Task<ApplicationUser> AddUserAsync(string userEmail, string userPW)
        {
            var found = await userManager.FindByEmailAsync(userEmail);

            if (found != null) return null;

            var user = new ApplicationUser
            {
                FirstName = fake.Person.FirstName,
                LastName = fake.Person.LastName,
                UserName = userEmail,
                Email = userEmail,
                TimeOfRegistration = DateTime.Now
            };

            var result = await userManager.CreateAsync(user, userPW);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return user;
        }

        private static List<Course> GetCourses() 
        {
            var courses = new List<Course>();

            for (int i =0; i < 15; i++)
            {
                int courseId = db.CourseClass.Select(c => c.Id).FirstOrDefault();
                string coursename = fake.Commerce.ProductName();
                coursename = coursename.Length < 25 ? coursename : coursename.Substring(0, 25);

                string description = fake.Commerce.ProductDescription();
                description = description.Length < 45 ? description : description.Substring(0, 45);
                var course = new Course
                {
                    CourseName = coursename,
                    Description = description, 
                    StartDate = System.DateTime.Now.AddDays(fake.Random.Int(-5,5)),
                    Modules = GetModules()
                    //Documents = GetDocuments()

                };
                courses.Add(course);
            }
            return courses;
        }

        private static ICollection<Module> GetModules()
        {
            var modules = new List<Module>();

            for (int i = 0; i < 20; i++)
            {
                string name = fake.Commerce.ProductName();
                name = name.Length < 25 ? name : name.Substring(0, 25);

                string description = fake.Lorem.Sentence();
                description = description.Length < 45 ? description : description.Substring(0, 45);
                var module = new Module
                {
                    Name = name,
                    Description =  description,
                    StartDate = System.DateTime.Now.AddDays(fake.Random.Int(-5, 5)),
                    EndDate = System.DateTime.Now.AddDays(fake.Random.Int(6, 16)),
                    Activities = GetActivities(),
                    //Documents = GetDocuments()
                };
                modules.Add(module);
            }
            return modules;
        }

        private static ICollection<Activity> GetActivities()
        {
            var activities = new List<Activity>();

            for (int i = 0; i < 20; i++)
            {
                string name = fake.Commerce.ProductName();
                name = name.Length < 25 ? name : name.Substring(0, 25);

                string description = fake.Lorem.Sentence();
                description = description.Length < 45 ? description : description.Substring(0, 45);
                Random rnd = new Random();
                int activitytypeid = rnd.Next(1, 5);
                var activity = new Activity
                {
                    Name = name,
                    Description = description,
                    StartDate = System.DateTime.Now.AddDays(fake.Random.Int(-5, 5)),
                    EndDate = System.DateTime.Now.AddDays(fake.Random.Int(6, 16)),
                    ActivityTypeId = activitytypeid,
                    //Documents = GetDocuments()
                };
                activities.Add(activity);
            }
            return activities;
        }

        //private static ICollection<Document> GetDocuments()
        //{
        //    var documents = new List<Document>();

        //    for (int i = 0; i < 20; i++)
        //    {
        //        string name = fake.Commerce.ProductName();
        //        name = name.Length < 25 ? name : name.Substring(0, 25);

        //        var document = new Document
        //        {
        //            Name = name,
        //            TimeStamp = DateTime.Now.AddDays(fake.Random.Int(1, 10))
        //        };
        //        documents.Add(document);
        //    }
        //    return documents;
        //}

        private static List<ApplicationUser> GetStudents()
        {
            var students = new List<ApplicationUser>();

            for (int i = 0; i < 15; i++)
            {
                var fName = fake.Name.FirstName();
                var lName = fake.Name.LastName();
                var email = fake.Internet.Email($"{fName}{lName}");
                Random rnd = new Random();
                int courseid = rnd.Next(1, 15);
                var student = new ApplicationUser
                {
                    FirstName = fName,
                    LastName = lName,
                    Email = email,
                    UserName = email,
                    TimeOfRegistration = DateTime.Now.AddDays(fake.Random.Int(-30, 0)),
                    CourseId = courseid

                };
                students.Add(student);
            }

            return students;
        }
    }
}
