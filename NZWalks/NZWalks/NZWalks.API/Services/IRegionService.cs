using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Services
{
    public interface IRegionService
    {
        Task<List<RegionDto>> GetAllAsync();

        Task<RegionDto?> GetByIdAsync(Guid id);

        Task<RegionDto> CreateAsync(RegionAddRequestDto createDto);

        Task<RegionDto?> UpdateAsync(Guid id, RegionUpdateRequestDto updateDto);

        Task<bool> DeleteAsync(Guid id);
    }
}