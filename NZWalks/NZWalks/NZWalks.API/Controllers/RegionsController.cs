using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Services;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {

        private readonly IRegionService regionService;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionService regionService, ILogger<RegionsController> logger)
        {
            this.logger = logger;
            this.regionService = regionService;
        }


        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<ActionResult<IEnumerable<RegionDto>>> GetAll()
        {
            logger.LogInformation("GetAll regions action was called");

            var regions = await regionService.GetAllAsync();

            logger.LogInformation($"GetAll regions action end with data: {JsonSerializer.Serialize(regions)}");
            return Ok(regions);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<ActionResult<RegionDto>> GetById([FromRoute] Guid id)
        {
            var region = await regionService.GetByIdAsync(id);
            if (region == null) return NotFound();

            return Ok(region);
        }


        [HttpPost]
        [Authorize(Roles = "Writer")]
        //[ValidateModel]
        public async Task<IActionResult> Create([FromBody] RegionAddRequestDto createDto)
        {

            var createdRegionDto = await regionService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdRegionDto.Id }, createdRegionDto);
        }


        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        //[ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RegionUpdateRequestDto updateDto)
        {

            var updatedRegionDto = await regionService.UpdateAsync(id, updateDto);
            if (updatedRegionDto == null) return NotFound();

            return Ok(updatedRegionDto);
        }


        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var wasRegionDeleted = await regionService.DeleteAsync(id);
            return wasRegionDeleted ? NoContent() : NotFound();
        }
    }
}