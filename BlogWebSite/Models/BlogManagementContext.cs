using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebSite.Models
{

    public class BlogManagementContext : DbContext
    {
        public BlogManagementContext()
        {
        }
        public BlogManagementContext(DbContextOptions<BlogManagementContext> options)
            : base(options)
        {
            LoadData();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<BlogDetail> BlogDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseInMemoryDatabase(databaseName: "BlogManagement");
            }
        }
        public void LoadData()
        {

        }
    }
}

