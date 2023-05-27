using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Services
{
    public class WalkService : IWalkService
    {

        private readonly IMapper mapper;

        private readonly IWalkRepository walkRepository;

        public WalkService(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        public async Task<WalkDto?> GetByIdAsync(Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            return mapper.Map<WalkDto>(walk);
        }

        public async Task<List<WalkDto>> GetAllAsync(string? filterOn, string? filterQuery,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000)
        {
            var walks = await walkRepository.GetWalks(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return mapper.Map<List<WalkDto>>(walks);
        }

        public async Task<WalkDto> CreateAsync(AddWalkRequestDto addWalkRequestDto)
        {
            var walkEntity = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walkEntity);

            return mapper.Map<WalkDto>(walkEntity);
        }

        public async Task<WalkDto?> UpdateAsync(Guid id, UpdateWalkRequestDto updateDto)
        {
            var existingWalk = await walkRepository.GetByIdAsync(id);
            if (existingWalk == null) return null;

            existingWalk.Description = updateDto.Description;
            existingWalk.Name = updateDto.Name;
            existingWalk.WalkImageUrl = updateDto.WalkImageUrl;
            existingWalk.LengthInKm = updateDto.LengthInKm;
            existingWalk.RegionId = updateDto.RegionId;
            existingWalk.DifficultyId = updateDto.DifficultyId;

            await walkRepository.UpdateAsync(existingWalk);

            return mapper.Map<WalkDto>(existingWalk);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingRegion = await walkRepository.GetByIdAsync(id);
            if (existingRegion == null) return false;

            await walkRepository.DeleteAsync(existingRegion);
            return true;
        }
    }
}