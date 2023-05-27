using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class PostgresWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public PostgresWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<IEnumerable<Walk>> GetWalks(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {

            var walksQuery = dbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .AsQueryable();
            
            // Filtering. TODO: Change to dynamic
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = walksQuery.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if(!string.IsNullOrWhiteSpace(sortBy))
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = isAscending ? walksQuery.OrderBy(x => x.Name) : walksQuery.OrderByDescending(x => x.Name);
                } else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = isAscending ? walksQuery.OrderBy(x => x.LengthInKm) : walksQuery.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walksQuery.Skip(skipResults).Take(pageSize).ToListAsync();
            /*return await dbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .ToListAsync();*/
        }


        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> UpdateAsync(Walk walk)
        {
            dbContext.Walks.Attach(walk);
            dbContext.Entry<Walk>(walk).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task DeleteAsync(Walk walk)
        {
            dbContext.Remove(walk);
            await dbContext.SaveChangesAsync();
        }
    }
}