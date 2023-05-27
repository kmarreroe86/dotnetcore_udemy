using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Services
{
    public interface IWalkService
    {
        Task<List<WalkDto>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy =null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000);

        Task<WalkDto?> GetByIdAsync(Guid id);

        Task<WalkDto> CreateAsync(AddWalkRequestDto addWalkRequestDto);

        Task<WalkDto?> UpdateAsync(Guid id, UpdateWalkRequestDto updateDto);

        Task<bool> DeleteAsync(Guid id);
    }
}