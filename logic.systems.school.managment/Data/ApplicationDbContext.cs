using logic.systems.school.managment.Helper;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define your entity sets (DbSets) here... 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.PaymentEnrollment)
            .WithOne(pe => pe.Enrollment)
            .HasForeignKey<EnrollmentPayment>(pe => pe.EnrollmentId);

            modelBuilder.Entity<Student>()
             .HasOne(s => s.CurrentSchoolLevel)
             .WithMany()
            .HasForeignKey(s => s.CurrentSchoolLevelId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
              .HasOne(s => s.SchoolClassRoom)
              .WithMany()
             .HasForeignKey(s => s.SchoolClassRoomId)
             .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Appsettings.DefaultConnection);
            }
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Tuition> Tuitions { get; set; }


        public DbSet<OrgUnitProvince> OrgUnitProvinces { get; set; }
        public DbSet<OrgUnitDistrict> OrgUnitDistricts { get; set; }

        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<TuitionFine> TuitionFines { get; set; }

        public DbSet<SimpleEntity> SimpleEntitys { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<EnrollmentItem> EnrollmentItems { get; set; }

        public DbSet<TuitionPayment> PaymentTuitions { get; set; } 
        public DbSet<EnrollmentPayment> PaymentEnrollments { get; set; }
        public DbSet<EnrollmentInvoice> EnrollmentInvoices { get; set; }
        public DbSet<TuitionInvoice> TuitionInvoices { get; set; }
        public DbSet<IdentityUser> IdentityUsers { get; set; }

    }
}