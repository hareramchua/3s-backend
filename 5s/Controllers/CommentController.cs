using System.Xml.Linq;
using _5s.Model;
using _5s.Services;
using Microsoft.AspNetCore.Mvc;

namespace _5s.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost(Name = "CreateComment")]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newComment = await _commentService.CreateComment(comment);
                return Ok(newComment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(Name = "GetAllComment")]
        public async Task<IActionResult> GetComment()
        {
            try
            {
                var comment = await _commentService.GetAllComment();
                if (comment == null || !comment.Any())
                {
                    return NotFound();
                }
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/comment", Name = "GetCommentById")]
        public async Task<IActionResult> GetComment(string id)
        {
            try
            {
                var comment = await _commentService.GetCommentById(id);
                if (comment == null)
                {
                    return NotFound();
                }
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateComment")]
        public async Task<IActionResult> UpdateComment(string id, [FromBody] Comment updateComment)
        {
            try
            {
                var dbComment = await _commentService.GetCommentById(id);
                if (dbComment == null)
                    return NotFound();
                var updatedRatings = await _commentService.UpdateComment(id, updateComment);
                return Ok(updatedRatings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            try
            {
                var dbComment = await _commentService.GetCommentById(id);
                if (dbComment == null)
                    return NotFound();
                await _commentService.DeleteComment(id);
                return Ok("Comment successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
