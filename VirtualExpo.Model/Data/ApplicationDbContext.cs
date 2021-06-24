using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Exhibition> Exhibitions { get; set; }
        public virtual DbSet<ExhibitorDescription> ExhibitorDescription { get; set; }
        public virtual DbSet<MediaLinks> MediaLinks { get; set; }
        public virtual DbSet<RequestAdmin> RequestAdmin { get; set; }
        public virtual DbSet<RequestOrganizer> RequestOrganizer { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<WorkExperience> WorkExperience { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }


        public virtual DbSet<AttendeeExhibitionJunction> AttendeeExhibitionJunctions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfiguration.ConnectionString, options => options.EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Education>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<ExhibitorDescription>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<Exhibition>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<MediaLinks>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<RequestOrganizer>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<RequestAdmin>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<Session>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<WorkExperience>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<AttendeeExhibitionJunction>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ContactUs>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
