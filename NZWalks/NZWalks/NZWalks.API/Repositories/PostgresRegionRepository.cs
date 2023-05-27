using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class PostgresRegionRepository : IRegionRepository
    {

        private readonly NZWalksDbContext dbContext;

        public PostgresRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions
                .FirstOrDefaultAsync(reg => reg.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Region region)
        {

            dbContext.Regions.Attach(region);
            dbContext.Entry<Region>(region).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task DeleteAsync(Region region)
        {
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
        }


    }
}