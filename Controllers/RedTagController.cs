using _5s.Model;
using _5s.Services;
using Microsoft.AspNetCore.Mvc;

namespace _5s.Controllers
{
    [Route("api/redtag")]
    [ApiController]
    public class RedTagController : Controller
    {
        private readonly IRedTagService _redTagService;
        public RedTagController(IRedTagService redTagService)
        {
            _redTagService = redTagService;
        }

        [HttpPost(Name = "CreateRedTag")]
        public async Task<IActionResult> CreateRedTag([FromBody] RedTag redtag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newRedTag = await _redTagService.CreateRedTag(redtag);

                return CreatedAtRoute("CreateRedTag", new { id = redtag.Id }, newRedTag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(Name = "GetAllRedTag")]
        public async Task<IActionResult> GetRedTag()
        {
            try
            {
                var redtag = await _redTagService.GetAllRedTag();
                if(redtag == null || !redtag.Any())
                {
                    return NotFound();
                }
                return Ok(redtag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/redtag", Name = "GetRedTagById")]
        public async Task<IActionResult> GetRedTag(string id)
        {
            try
            {
                var redtag = await _redTagService.GetRedTagById(id);
                if (redtag == null)
                {
                    return NoContent();
                }
                return Ok(redtag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateRedTag")]
        public async Task<IActionResult> UpdateRedTag(string id, [FromBody] RedTag redtag)
        {
            try
            {
                var dbRedTag = await _redTagService.GetRedTagById(id);
                if (dbRedTag == null)
                    return NotFound();
                var updatedRedTag = await _redTagService.UpdateRedTag(id, redtag);
                return Ok(updatedRedTag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{name}", Name = "DeleteRedTag")]
        public async Task<IActionResult> DeleteRedTag(string name)
        {
            try
            {
                var dbRoom = await _redTagService.GetRedTagByName(name);
                if (dbRoom == null)
                    return NotFound();
                await _redTagService.DeleteRedTag(dbRoom.Id);
                return Ok("RedTag successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
