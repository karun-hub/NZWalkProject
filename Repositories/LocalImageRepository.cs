using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DemoDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, DemoDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath,"Images",$"{image.FileName}{image.FileExtension}");
            // Upload Image to local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/images/image.jpg
             var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
             image.FilePath = urlFilePath;

             // add the image details to the table
             await dbContext.Images.AddAsync(image);
             await dbContext.SaveChangesAsync();
             return image;
        }
    }
}