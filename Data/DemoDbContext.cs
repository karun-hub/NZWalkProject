using AutoMapper.Configuration.Annotations;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulties/*table name*/ { get; set; }
        public  DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walk { get; set; }
        public DbSet<Demon> demon {get; set;}
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // sedd the data for difficulties
            //Easy,Medium,Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id=Guid.Parse("6ce32b59-05e3-4aba-a231-91ddea390108"),
                    Name="Easy"
                },
                new Difficulty()
                {
                    Id=Guid.Parse("cde34ce4-57c8-4033-bded-247effe05523"),
                    Name="Medium"
                },
                new Difficulty()
                {
                    Id=Guid.Parse("64fdf159-255e-4b73-87a4-8f7f7ebd40a3"),
                    Name="Hard"
                }
                
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id= Guid.Parse("14d53e48-1449-4aa1-ae15-cdc8487dd828"),
                    Name= "Kochi",
                    Code="Koc",
                    RegionImageUrl="Kochi.jpg"
                },
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