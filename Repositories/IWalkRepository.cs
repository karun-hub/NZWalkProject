using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk>CreatAsync(Walk walk);
        Task<List<Walk>>GetAllAsync(string?filterOn= null, string?filterQuery= null,string? sortBy= null, bool isAscending =true, int pageNumber=1,int PageSize=1000);
        Task<Walk?>GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id,Walk walk);
        Task<Walk?> DeleteAsync(Guid id );
    }
}