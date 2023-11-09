using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Demo.Models.DTO;
using Demo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid){

                //converting dto to domain model
                 var image= new Image{
                    File= request.File,
                    FileExtension= Path.GetExtension(request.File.FileName),
                    FileSizeInBytes= request.File.Length,
                    FileName=request.FileName,
                    FileDescription= request.FileDescription
                 };
                //use repository to upload image
                await imageRepository.Upload(image);
                return Ok(image);
            }
            return BadRequest(ModelState);

        }
        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[]{ ".jpg",".jpeg",".png"};
            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file","Unsupported file  format");
            }
            if(request.File.Length> 10485760)
            {
                 ModelState.AddModelError("file","Larger file size , please upload a smaller size of image.");
            }
        }
    }
}