using System.Text.Json;
using AutoMapper;
using Demo.CustomActionFilters;
using Demo.Data;
using Demo.Models;
using Demo.Models.DTO;
using Demo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionController : Controller
    {
        private readonly IRegionRepository regionRepository ;
        private readonly IMapper mapper;
        private readonly DemoDbContext dbContext;
        private readonly ILogger<RegionController> logger;
        public RegionController(DemoDbContext dbContext, IRegionRepository regionRepository,IMapper mapper, ILogger<RegionController> logger)
        {
            this.logger = logger;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
            
        }
        [HttpGet]
        //[Authorize(Roles ="Writer,Reader")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAll Regions Action Method was invoked");
            //Get data from the database
            logger.LogWarning("This is a warning log hehe");
            logger.LogError("This is a dummy error hehe");
           var regions= await regionRepository.GetAllAsync();
            // map dmoain model to dto
            // var regionDto= new List<RegionDto>();
            // foreach ( var regionDomain in regions)
            // {
            //     regionDto.Add(new RegionDto()
            //     {
            //         Id= regionDomain.Id,
            //         Code= regionDomain.Code,
            //         Name = regionDomain.Name,
            //         RegionImageUrl= regionDomain.RegionImageUrl
            //     });
            // }
            var regionDto =mapper.Map<List<RegionDto>>(regions);
            logger.LogInformation($"Fininshed GetAllRegion request with data: {JsonSerializer.Serialize(regions)}");
            // return dto
             return Ok(regionDto);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Writer,Reader")]
        public async Task<IActionResult> GetregionById([FromRoute]Guid id)
        {
           // var region = dbContext.Regions.Find(id);
           // get region from database
           var region = await regionRepository.GetByIdAsync(id);
            if( region == null)
            {
                return NotFound();
            }
            // map region doimain model to Dto
            
             return Ok(mapper.Map<RegionDto>(region)); 
        

        }
        [HttpPost] 
        [ValidateModel]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO )
        {
           
                 //Map or convert DTO to Domain Model
                var region= mapper.Map<Region>(addRegionRequestDTO);
                region= await regionRepository.CreateAsync(region);
                 //Map Domain Model to DTO
                 var regiondto=  mapper.Map<RegionDto>(region);
                return CreatedAtAction(nameof(GetregionById),new{ id= regiondto.Id},regiondto);
           } 
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles ="Writer")]
         public  async Task<IActionResult> Update( [FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
         {  
            
                var regionDomainModel= mapper.Map<Region>(updateRegionDTO);
              regionDomainModel = await regionRepository.UpdateAsync(id,regionDomainModel);
              if ( regionDomainModel == null)
              {
                return NotFound();
              }
                var  regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);
            
         } 
         [HttpDelete]
         [Route("{id:Guid}")]
         [Authorize(Roles ="Writer")]
         public  async Task<IActionResult> Delete([FromRoute] Guid  id)
         {
             var regionDomainModel = await regionRepository.DeleteAsync(id);
             if ( regionDomainModel== null)
             {
                 return NotFound();
             }

              var regionDto = mapper.Map<RegionDto>(regionDomainModel);
               return Ok(regionDto);

         }
    }
}