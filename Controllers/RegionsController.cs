using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyCourse.API.Data;
using UdemyCourse.API.Models.Domain;
using UdemyCourse.API.Models.DTO;
using UdemyCourse.API.Repositories;

namespace UdemyCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly UdemyCourseDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(UdemyCourseDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            //get data from db - Domain models
            var regions = await regionRepository.GetAllAsync();

            // map doamin models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {


            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
                return NotFound();

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);


        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionDto regionDto)
        {

            var region = new Region()
            {
                Id = new Guid(),
                Code = regionDto.Code,
                Name = regionDto.Name,
                RegionImageUrl = regionDto.RegionImageUrl,

            };

            region = await regionRepository.CreateAsync(region);

            var newRegionDto = new RegionDto()
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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto regionDto)
        {

            var region = new Region()
            {
                Id = id,
                Code = regionDto.Code,
                Name = regionDto.Name,
                RegionImageUrl = regionDto.RegionImageUrl,

            };

            var exist = await regionRepository.UpdateAsync(id, region);
            if (exist == null)
                return NotFound();


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
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {


            var exist = await regionRepository.DeleteAsync(id);
            if (exist == null)
                return NotFound();

            var deletedRegionDto = new RegionDto()
            {
                Id = exist.Id,
                Code = exist.Code,
                Name = exist.Name,
                RegionImageUrl = exist.RegionImageUrl,


            };


            return Ok(deletedRegionDto);


        }
    }
}
