using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NZWalks.API.Extensions;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Services
{
    public class RegionService : IRegionService
    {

        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionService(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        public async Task<List<RegionDto>> GetAllAsync()
        {

            var regionsModel = await regionRepository.GetAllAsync();

            // return regionsModel.Select(reg => reg.RegionAsDto()).ToList();

            // mapping domain model to dto
            return mapper.Map<List<RegionDto>>(regionsModel);
        }

        public async Task<RegionDto?> GetByIdAsync(Guid id)
        {
            var regionEntity = await regionRepository.GetByIdAsync(id);
            if (regionEntity == null) return null;

            // return regionEntity.RegionAsDto();
            return mapper.Map<RegionDto>(regionEntity);
        }


        public async Task<RegionDto> CreateAsync(RegionAddRequestDto createDto)
        {
            // var regionEntity = new Region()
            // {
            //     Code = createDto.Code,
            //     Name = createDto.Name,
            //     RegionImageUrl = createDto.RegionImageUrl
            // };
            var regionEntity = mapper.Map<Region>(createDto);

            await regionRepository.CreateAsync(regionEntity);
            // return regionEntity.RegionAsDto();
            return mapper.Map<RegionDto>(regionEntity);
        }

        public async Task<RegionDto?> UpdateAsync(Guid id, RegionUpdateRequestDto updateDto)
        {
            var existingRegion = await regionRepository.GetByIdAsync(id);
            if (existingRegion == null) return null;

            existingRegion.Code = updateDto.Code;
            existingRegion.Name = updateDto.Name;
            existingRegion.RegionImageUrl = updateDto.RegionImageUrl;
            await regionRepository.UpdateAsync(existingRegion);

            return existingRegion.RegionAsDto();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingRegion = await regionRepository.GetByIdAsync(id);
            if (existingRegion == null) return false;

            await regionRepository.DeleteAsync(existingRegion);
            return true;
        }
    }
}