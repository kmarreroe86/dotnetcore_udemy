using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Services;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class WalksController : ControllerBase
    {

        private readonly IWalkService walkService;

        public WalksController(IWalkService walkService)
        {
            this.walkService = walkService;
        }


        [HttpGet]
        //GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        public async Task<ActionResult<IEnumerable<WalkDto>>> GetWalks(
            [FromQuery] string? filterOn, string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {

            // throw exception for testing ExceptionHandlerMiddleware
            throw new Exception("Test exception");
            return await walkService.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<WalkDto>> GetById([FromRoute] Guid id)
        {
            var walk = await walkService.GetByIdAsync(id);
            if (walk == null) return NotFound();

            return Ok(walk);
        }

        [HttpPost]
        //[ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto walkRequestDto)
        {
            var savedWalk = await walkService.CreateAsync(walkRequestDto);
            return Ok(savedWalk);// change for CreatedAt
        }

        [HttpPut("{id:Guid}")]
        //[ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateDto)
        {
            var walkRegionDto = await walkService.UpdateAsync(id, updateDto);
            if (walkRegionDto == null)
                return NotFound();
            return Ok(walkRegionDto);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var wasWalkDeleted = await walkService.DeleteAsync(id);
            return wasWalkDeleted ? NoContent() : NotFound();
        }

    }
}