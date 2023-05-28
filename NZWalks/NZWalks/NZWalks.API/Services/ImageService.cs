using Microsoft.AspNetCore.Mvc.ModelBinding;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Services
{
    public class ImageService : IImagesService
    {
        private readonly string[] ALLOWED_EXTENSIONS = { ".jpg", ".jpeg", ".png" };

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IImageRepository imageRepository;
        private readonly IHttpContextAccessor httpCtxAccessor;

        public ImageService(IWebHostEnvironment webHostEnvironment, IImageRepository imageRepository,
        IHttpContextAccessor httpContextAccessor)
        {
            this.httpCtxAccessor = httpContextAccessor;
            this.webHostEnvironment = webHostEnvironment;
            this.imageRepository = imageRepository;
        }


        public void ValidateFileUpload(ImageRequestDto requestDto, ModelStateDictionary modelState)
        {
            if (!ALLOWED_EXTENSIONS.Contains(Path.GetExtension(requestDto.FileName)))
            {
                modelState.AddModelError("file", "Unsupported file extnsion");
            }
            if (requestDto.File.Length > 10485760)
            {
                modelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file");
            }
        }

        public async Task<Image> Create(ImageRequestDto requestDto)
        {
            var fileExt = Path.GetExtension(requestDto.FileName);
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{requestDto.FileName}");
            var imageDomainModel = new Image
            {
                File = requestDto.File,
                FileExtension = fileExt,
                FileSizeInBytes = requestDto.File.Length,
                FileName = requestDto.FileName,
                FileDescription = requestDto.FileDescription
            };

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await imageDomainModel.File.CopyToAsync(stream);

            var urlFilePath = $"{httpCtxAccessor.HttpContext.Request.Scheme}://{httpCtxAccessor.HttpContext.Request.Host}{httpCtxAccessor.HttpContext.Request.PathBase}/Images/{imageDomainModel.FileName}{imageDomainModel.FileExtension}";
            imageDomainModel.FilePath = urlFilePath;

            return await imageRepository.Create(imageDomainModel);
        }
    }
}