using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.Models.Domain;

namespace NewZealandWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,
            NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }



        public async Task<Image> Upload(Image image)
        {
        var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",$"{image.FileName}{image.FileExtension}");

            //Uploading image to the local path in images folder
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";


            image.FilePath = urlFilePath;

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;

           

        }
    }
}
