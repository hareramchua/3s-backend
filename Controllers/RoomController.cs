using _5s.Model;
using _5s.Services;
using Microsoft.AspNetCore.Mvc;

namespace _5s.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost(Name = "CreateRoom")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newRoom = await _roomService.CreateRoom(room);
                return CreatedAtRoute("GetRoomById", new { id = room.Id }, newRoom);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(Name = "GetAllRoom")]
        public async Task<IActionResult> GetRoom()
        {
            try
            {
                var room = await _roomService.GetAllRoom();
                if(room == null || !room.Any())
                {
                    return NotFound();
                }
                return Ok(room);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/room", Name = "GetRoomById")]
        public async Task<IActionResult> GetRoom(string id)
        {
            try
            {
                var room = await _roomService.GetRoomById(id);
                if (room == null)
                {
                    return NotFound();
                }
                return Ok(room);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateRoom")]
        public async Task<IActionResult> UpdateRoom(string id, [FromBody] Room room)
        {
            try
            {
                var dbRoom = await _roomService.GetRoomById(id);
                if (dbRoom == null)
                    return NotFound();
                var updatedRoom = await _roomService.UpdateRooms(id, room);
                return Ok(updatedRoom);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{name}", Name = "DeleteRoom")]
        public async Task<IActionResult> DeleteRoom(string roomNumber)
        {
            try
            {
                var dbRoom = await _roomService.GetRoomByRoomNumber(roomNumber);
                if (dbRoom == null)
                    return NotFound();
                await _roomService.DeleteRoom(dbRoom.Id);
                return Ok("Room successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
