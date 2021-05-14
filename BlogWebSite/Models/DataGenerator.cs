using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebSite.Models
{   
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BlogManagementContext(
                serviceProvider.GetRequiredService<DbContextOptions<BlogManagementContext>>()))
            {


                if (context.Authors.Any())
                {
                    return;   // Data was already seeded
                }
                context.Authors.AddRange(
               new Author
               {
                   AuthorId = 1,
                   UserName = "Srishti Gupta",
                   Password = "Assignment",
                   CreatedBy = "Srishti Gupta",
                   CreatedAt = DateTime.Now
               },
               new Author
               {
                   AuthorId = 2,
                   UserName = "Admin",
                   Password = "Grapecity",
                   CreatedBy = "Admin",
                   CreatedAt = DateTime.Now
               });

                if (context.BlogDetails.Any())
                {
                    return;   // Data was already seeded
                }
                context.BlogDetails.AddRange(
               new BlogDetail
               {
                   BlogId = 1,
                   Title = "First Blog",
                   BlogContent = "Welcome to my blog!! ",
                   Category = "General Info",
                   AuthorId = 1,
                   CreatedBy = "Srishti Gupta"

               },
               new BlogDetail
               {
                   BlogId = 2,
                   Title = "Second Blog",
                   BlogContent = "Welcome to Second blog!! ",
                   Category = "Food",
                   AuthorId = 2,
                   CreatedBy = "Admin"
               });
                context.SaveChanges();

            }
        }
    }
}
