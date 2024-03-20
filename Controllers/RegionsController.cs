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

            return CreatedAtAction(nameof(GetById), new { id = newRegionDto.Id }, newRegionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDto regionDto)
        {
            var exist = dbContext.Regions.Find(id);
            if (exist == null)
                return BadRequest();

           
            exist.Code = regionDto.Code;
            exist.Name = regionDto.Name;
            exist.RegionImageUrl = regionDto.RegionImageUrl;
              
            
            // dont have to do this bcs of exist is being track in db (we dont create new obcjet)
           // dbContext.Regions.Update(exist);
            dbContext.SaveChanges();

            var returnedRegion = new RegionDto()
            {
                Id = exist.Id,
                Code = exist.Code,
                Name = exist.Name,
                RegionImageUrl = exist.RegionImageUrl,
            };


            return Ok(returnedRegion);


        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id) {

            var exist = dbContext.Regions.Find(id);

            if (exist == null)
                return NotFound();

            var deletedRegionDto = new RegionDto()
            {
                Id = exist.Id,
                Code = exist.Code,
                Name = exist.Name,
                RegionImageUrl = exist.RegionImageUrl,


            };

            dbContext.Regions.Remove(exist);
            dbContext.SaveChanges();

            return Ok(deletedRegionDto);
        
        }
    }
}
