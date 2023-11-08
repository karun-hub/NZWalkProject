using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly DemoDbContext dbContext;
        public SQLWalkRepository(DemoDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }
        public async Task<Walk> CreatAsync(Walk walk)
        {
            await dbContext.Walk.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var  walktoDelete= await dbContext.Walk.FirstOrDefaultAsync(x=> x.Id == id);
            if( walktoDelete ==null)
            {
                return null;
            }
             dbContext.Walk.Remove(walktoDelete);
             await dbContext.SaveChangesAsync();
             return walktoDelete;
        }

        public async Task<List<Walk>> GetAllAsync(string?filterOn= null, string?filterQuery= null,string? sortBy= null, bool isAscending =true,int pageNumber=1,int PageSize=1000)
        {
            var walks=dbContext.Walk.Include("Difficulty").Include("Region").AsQueryable();
            //filtering
            if(string.IsNullOrWhiteSpace(filterOn)== false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks= walks.Where(c=> c.Name.Contains(filterQuery));
                }
            }
            // sorting
            if(string.IsNullOrWhiteSpace(sortBy)== false)
            {
                 if(sortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                 {
                    walks= isAscending ? walks.OrderBy(x=>x.Name):walks.OrderByDescending(x=>x.Name);
                 }
                 else if(sortBy.Equals("Length",StringComparison.OrdinalIgnoreCase))
                 {
                    walks = isAscending? walks.OrderBy(x=> x.LengthInKm): walks.OrderByDescending(x=> x.LengthInKm);
                 }
            }
            //Pagination
            var skipresults = (pageNumber-1) * PageSize;

            return await walks.Skip(skipresults).Take(PageSize).ToListAsync();
           // return await dbContext.Walk.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await dbContext.Walk
            .Include("Difficulty")
            .Include("Region")
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id,Walk walk)
        {
            var existingwalk= await dbContext.Walk.FirstOrDefaultAsync(x=> x.Id==id);
            if(existingwalk== null)
            {
                return null;
            }
             existingwalk.Name= walk.Name;
             existingwalk.Description= walk.Description;
             existingwalk.LengthInKm= walk.LengthInKm;
             existingwalk.WalkImageUrl=walk.WalkImageUrl;
             existingwalk.RegionId= walk.RegionId;
             existingwalk.DifficultyId= walk.DifficultyId;
             await dbContext.SaveChangesAsync();
             return existingwalk;

        }
    }
}