using AutoMapper;
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
        private readonly IMapper mapper;

        public RegionsController(UdemyCourseDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {


            var regions = await regionRepository.GetAllAsync();

            var regionsDto =mapper.Map<List<RegionDto>>(regions); 

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

            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionDto regionDto)
        {

            var region = mapper.Map<Region>(regionDto);

            region = await regionRepository.CreateAsync(region);

            var newRegionDto = mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetById), new { id = newRegionDto.Id }, newRegionDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto regionDto)
        {

            var region = mapper.Map<Region>(regionDto);

            var exist = await regionRepository.UpdateAsync(id, region);
            if (exist == null)
                return NotFound();


            var returnedRegion = mapper.Map<RegionDto>(exist);

            return Ok(returnedRegion);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {


            var exist = await regionRepository.DeleteAsync(id);
            if (exist == null)
                return NotFound();

            var deletedRegionDto = mapper.Map<RegionDto>(exist);


            return Ok(deletedRegionDto);
        }
    }
}
