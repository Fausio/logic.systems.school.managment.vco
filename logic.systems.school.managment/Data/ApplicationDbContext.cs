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
            base.OnModelCreating(modelBuilder);


            //if (this.Users.Count() <=0)
            //{
            //    //Seeding a  'Administrator' role to AspNetRoles table
            //    modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            //    {
            //        Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
            //        Name = "Administrator",
            //        NormalizedName = "ADMINISTRATOR".ToUpper()
            //    });


            //    //a hasher to hash the password before seeding the user to the db
            //    var hasher = new PasswordHasher<IdentityUser>();

            //    //Seeding the User to AspNetUsers table
            //    modelBuilder.Entity<IdentityUser>().HasData(
            //        new IdentityUser
            //        {
            //            Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
            //            UserName = "Administrator",
            //            NormalizedUserName = "Administrator",
            //            Email = "developer@logicsystems.co.mz",
            //            NormalizedEmail = "developer@logicsystems.co.mz",
            //            PhoneNumber = "+258 872023200",
            //            EmailConfirmed = true,
            //            PhoneNumberConfirmed = true,
            //            SecurityStamp = Guid.NewGuid().ToString("D"),
            //            PasswordHash = hasher.HashPassword(null, "Madara1122**")
            //        }
            //    );

            //    //Seeding the relation between our user and role to AspNetUserRoles table
            //    modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //        new IdentityUserRole<string>
            //        {
            //            RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
            //            UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            //        }
            //    );
            //}

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Appsettings.DefaultConnection);
            }
        }

        public DbSet<Student> Students { get; set; }
        //public DbSet<SchoolLevel> SchoolLevels { get; set; }
        public DbSet<Tuition> Tuitions { get; set; }


        public DbSet<OrgUnitProvince> OrgUnitProvinces { get; set; }
        public DbSet<OrgUnitDistrict> OrgUnitDistricts { get; set; }

        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<TuitionFines> TuitionFines { get; set; }

        public DbSet<SimpleEntity> SimpleEntitys { get; set; }
         public DbSet<Payment> Payments { get; set; }



    }
}