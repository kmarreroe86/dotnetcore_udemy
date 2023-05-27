using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Services;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {

        private readonly IImagesService imageService;

        public ImagesController(IImagesService imageService)
        {
            this.imageService = imageService;
        }


        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageRequestDto imageFile)
        {
            imageService.ValidateFileUpload(imageFile, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var savedImage = await imageService.Create(imageFile);
            return Ok(savedImage);
        }
    }
}