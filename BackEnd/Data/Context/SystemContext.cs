using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>();
            var user = new User()
            {
                Id = 1,
                UserName = "Ahmed",
                Email = "ahmed@gmail.com".ToUpper(),
                Role = "Instructor",
            };
            user.Password = hasher.HashPassword(user, "PPy%vhM7");
            modelBuilder.Entity<User>()
                .HasData(user);

            modelBuilder.Entity<Instructor>()
                .HasData(new Instructor
                {
                    Id = 1,
                    Name = "Ahmed",
                    Specialty = "Software Development",
                    UserId = user.Id,
                });
            
        }
    }
}
