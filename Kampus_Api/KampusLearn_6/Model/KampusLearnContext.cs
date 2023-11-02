using System;
using KampusLearn_6.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CaseStudyKampusLearnAPI.Models
{
    public partial class KampusLearnContext : DbContext
    {
        public KampusLearnContext()
        {
        }

        public KampusLearnContext(DbContextOptions<KampusLearnContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<CandidateCourse> CandidateCourse { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Progress> Progress { get; set; }
        public virtual DbSet<Scheduling> Scheduling { get; set; }
        public virtual DbSet<Trainer> Trainer { get; set; }
        public virtual DbSet<TrainerCourse> TrainerCourse { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=LAPTOP-0MQC0OIN;Database=KampusLearn6; integrated security=true; TrustServerCertificate = True");


        }
    }
}