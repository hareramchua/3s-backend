using _5s.Model;
using _5s.Services;
using Microsoft.AspNetCore.Mvc;


namespace _5s.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceImageController : ControllerBase
    {
        private readonly ISpaceImageService _spaceImageService;

        public SpaceImageController(ISpaceImageService spaceImageService)
        {
            _spaceImageService = spaceImageService;
        }

        [HttpPost]
        [Route("upload/{spaceId}")]
        public async Task<IActionResult> UploadSpaceImage(string spaceId, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Invalid file.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var spaceImage = new SpaceImage
                    {
                        SpaceId = spaceId,
                        Image = memoryStream.ToArray()
                    };

                    var imageId = await _spaceImageService.CreateSpaceImage(spaceImage);
                    return Ok(new { ImageId = imageId });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get/{spaceId}")]
        public async Task<IActionResult> GetSpaceImages(string spaceId)
        {
            try
            {
                var spaceImages = await _spaceImageService.GetAllSpaceImagesBySpaceId(spaceId);

                if (spaceImages == null || !spaceImages.Any())
                {
                    return NotFound("Images not found.");
                }

                //var imageIds = spaceImages.Select(image => image.Id);
                return Ok(spaceImages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("delete/{imageId}")]
        public async Task<IActionResult> DeleteSpaceImage(string imageId)
        {
            try
            {
                await _spaceImageService.DeleteSpaceImage(imageId);
                return Ok("Image deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
