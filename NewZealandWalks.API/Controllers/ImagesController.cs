using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Repositories;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload ([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if(ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName= request.FileName,
                    FileDescription = request.FileDescription,
                };

                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg" ,".png"};
            if (!allowedExtension.Contains(Path.GetExtension(request.File.FileName))) {
                ModelState.AddModelError("file","Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "file size mor ethan 10MB please upload smaller file");
            }
        }
    }
}
