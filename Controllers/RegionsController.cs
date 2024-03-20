using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCourse.API.Data;
using UdemyCourse.API.Models.Domain;
using UdemyCourse.API.Models.DTO;

namespace UdemyCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly UdemyCourseDbContext dbContext;

        public RegionsController(UdemyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            //get data from db - Domain models
            var regions = dbContext.Regions.ToList();

            // map doamin models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto() { 
                    Id = region.Id, 
                    Code = region.Code,
                    Name = region.Name, 
                    RegionImageUrl = region.RegionImageUrl
                });
            }


            //return DTOs not Domain model
            return Ok(regionsDto);
        }

        [HttpGet]
        //take id from route e.g /api/regions/{id}
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {

            //CAN ONLY BE USED WITH PRIMARY KEY
            //var region=dbContext.Regions.Find(id);
            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (region == null)
                return NotFound();

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name= region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }


        [HttpPost]
        public IActionResult Create([FromBody] AddRegionDto regionDto)
        {
            var region = new Region()
            {
                Id = new Guid(),
                Code = regionDto.Code,
                Name = regionDto.Name,
                RegionImageUrl = regionDto.RegionImageUrl,

            };

            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

            var newRegionDto= new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
                

            };

            return CreatedAtAction("test",  newRegionDto);
        }
    }
}
