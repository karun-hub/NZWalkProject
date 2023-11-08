using AutoMapper;
using Demo.CustomActionFilters;
using Demo.Models;
using Demo.Models.DTO;
using Demo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    public class WalksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository ;
        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            
        }
       //create walk
       [HttpPost]
       [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            
                //Map Dto to Domain Model
            var walks= mapper.Map<Walk>(addWalkRequestDTO);
            await walkRepository.CreatAsync(walks);
            // map Domain Model to  dto

             return Ok(mapper.Map<WalkDTO>(walks));
            

        }

        //get the walk
        //Get : /api/walks/filterOn= Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string? filterOn, [FromQuery] string? filterQuery,[FromQuery]string? sortBy, [FromQuery] bool? isAscending,[FromQuery] int pageNumber=1,[FromQuery] int pageSize= 1000)
        {
            var walks= await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending?? true,pageNumber,pageSize);
            return Ok(mapper.Map<List<WalkDTO>>(walks));
        }
        [HttpGet("id")]
        // [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
                var walk= await walkRepository.GetByIdAsync(id);
                if(walk== null)
                {
                    return NotFound();
                }
                //mapper domain modle to a DTo
                return Ok( mapper.Map<WalkDTO>(walk));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult>Update([FromRoute] Guid id,[FromBody] UpdateWalkDTO updateWalkDTO)
        {
            //Map dto to domain model
            var walk = mapper.Map<Walk>(updateWalkDTO);
            walk= await walkRepository.UpdateAsync(id,walk);
            if( walk== null)
            {
                return NotFound();
            }
             return Ok(mapper.Map<WalkDTO>(walk));

        }
        // Delete walk
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult>Delete([FromRoute]Guid  id)
        {
            var deletedWalk = await walkRepository.DeleteAsync(id);
            if( deletedWalk== null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(deletedWalk));
        }
    }
}
