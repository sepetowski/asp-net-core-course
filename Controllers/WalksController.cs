using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCourse.API.Models.Domain;
using UdemyCourse.API.Models.DTO;
using UdemyCourse.API.Repositories;

namespace UdemyCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var walkList = await walkRepository.GetAllAsync();

            var returnWalkList = mapper.Map<List<WalkDto>>(walkList);

            return Ok(returnWalkList);
        }  

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddWalkDto addWalkDto)
        {
          var walk= mapper.Map<Walk>(addWalkDto);

          await walkRepository.CreateAsync(walk);

          var returnWalk = mapper.Map<WalkDto>(walk);

          return Ok(returnWalk);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walk= await walkRepository.GetByIdAsync(id);
            if (walk == null)
                return NotFound();


            var returnWalk= mapper.Map<WalkDto>(walk);

            return Ok(returnWalk);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkDto updatedWalk)
        {

            var walk = mapper.Map<Walk>(updatedWalk);
            var exist =await walkRepository.UpdateAsync(id,walk);

            if (exist == null)
                return NotFound();

            var returnWalk= mapper.Map<WalkDto>(exist);
            return Ok(returnWalk);  

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {


            var exist = await walkRepository.DeleteAsync(id);
            if (exist == null)
                return NotFound();

            var deletedWalk = mapper.Map<WalkDto>(exist);


            return Ok(deletedWalk);
        }
    }
}
