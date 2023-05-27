using NZWalks.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Services
{
    public interface IImagesService
    {
        void ValidateFileUpload(ImageRequestDto requestDto, ModelStateDictionary modelState);

        Task<Image> Create(ImageRequestDto requestDto);
    }
}