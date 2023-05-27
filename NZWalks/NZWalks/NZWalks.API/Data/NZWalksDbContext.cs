using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : IdentityDbContext /*DbContext*/
    {

        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Seed Roles
            var readerRole = "cfc29908-ecab-4b04-848d-a6796c36d97a";
            var writerRole = "780cd901-4045-4d25-b808-7d28a92b498f";
            var roles = new List<IdentityRole> { 
                    new IdentityRole
                    {
                        Id = readerRole,
                        ConcurrencyStamp = readerRole,
                        Name = "Reader",
                        NormalizedName = "Reader".ToUpper(),
                    },
                    new IdentityRole
                    {
                        Id = writerRole,
                        ConcurrencyStamp = writerRole,
                        Name = "Writer",
                        NormalizedName = "Writer".ToUpper(),
                    }
                };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Seed data for Difficulties
            var difficulties = new List<Difficulty>
            {
                new Difficulty() {
                    Id = Guid.Parse("c8e1db2c-ae26-4d1c-8c15-d206b951edfa"),
                    Name = "Easy"
                },
                new Difficulty() {
                    Id = Guid.Parse("76a2ea0d-a9a1-4b67-acc2-1d5f0846d4fa"),
                    Name = "Medium"
                },
                new Difficulty() {
                    Id = Guid.Parse("da27be4e-4ab8-43dd-b534-0dfd4913d0fb"),
                    Name = "Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}